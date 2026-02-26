using Mojo.Application.DTOs.EntitiesDto.Contrat.Validators;
using MediatR;
using AutoMapper;
using Mojo.Application.DTOs.Common;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Contrats.Handler.Command
{
    public class UpdateContratHandler : IRequestHandler<UpdateContratCommand, BaseResponse>
    {
        private readonly IContratRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAmortissementRepository _amortissementRepository;
        private readonly IMoisAmortissementRepository _moisAmortissementRepository;

        public UpdateContratHandler(
            IContratRepository repository,
            IMapper mapper,
            IVeloRepository veloRepository,
            IUserRepository userRepository,
            IAmortissementRepository amortissementRepository,
            IMoisAmortissementRepository moisAmortissementRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
            _userRepository = userRepository;
            _amortissementRepository = amortissementRepository;
            _moisAmortissementRepository = moisAmortissementRepository;
        }

        public async Task<BaseResponse> Handle(UpdateContratCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            Console.WriteLine("========== DEBUT UPDATE CONTRAT ==========");

            // Validation
            var validator = new ContratValidator(_veloRepository, _userRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("default", "Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la modification du contrat : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                Console.WriteLine("[ERREUR] Validation échouée");
                return response;
            }

            // Récupérer le contrat existant
            var oldContrat = await _repository.GetByIdAsync(request.dto.Id);
            if (oldContrat == null)
            {
                response.Success = false;
                response.Message = "Echec de la modification du contrat.";
                response.Errors.Add($"Aucun contrat trouvé avec l'Id {request.dto.Id}.");
                Console.WriteLine($"[ERREUR] Contrat {request.dto.Id} introuvable");
                return response;
            }

            Console.WriteLine($"[INFO] Contrat trouvé - ID: {oldContrat.Id}, VeloId: {oldContrat.VeloId}");
            Console.WriteLine($"[INFO] Ancienne durée: {oldContrat.Duree} mois");
            Console.WriteLine($"[INFO] Nouvelle durée: {request.dto.Duree} mois");

            // Vérifier si la durée a changé
            bool dureeChanged = oldContrat.Duree != request.dto.Duree;
            int oldDuree = oldContrat.Duree;
            int newDuree = request.dto.Duree;

            Console.WriteLine($"[INFO] Durée changée ? {(dureeChanged ? "OUI" : "NON")}");

            // Mettre à jour le contrat
            _mapper.Map(request.dto, oldContrat);
            await _repository.UpdateAsync(oldContrat);
            Console.WriteLine("[INFO] Contrat mis à jour dans la base");

            // Si la durée a changé, mettre à jour l'amortissement associé
            if (dureeChanged)
            {
                var montantMensuel = request.dto.MontantAmortissementMensuel ?? request.dto.LoyerMensuelHT;
                Console.WriteLine($"[INFO] Recherche de l'amortissement pour VeloId: {oldContrat.VeloId}");

                var amortissements = await _amortissementRepository.GetAllAsync();
                Console.WriteLine($"[INFO] Nombre total d'amortissements: {amortissements.Count()}");

                var amortissement = amortissements.FirstOrDefault(a =>
                    a.VeloId == oldContrat.VeloId && a.IsActif);

                if (amortissement != null)
                {
                    Console.WriteLine($"[INFO] Amortissement trouvé - ID: {amortissement.Id}");
                    Console.WriteLine($"[INFO] Valeurs actuelles - DureeMois: {amortissement.DureeMois}, ValeurInit: {amortissement.ValeurInit}, ValeurResiduelle: {amortissement.ValeurResiduelleFinale}");

                    // Mettre à jour la durée de l'amortissement
                    amortissement.DureeMois = newDuree;

                    // Recalculer la valeur résiduelle finale selon le montant mensuel
                    amortissement.ValeurResiduelleFinale = amortissement.ValeurInit - (montantMensuel * newDuree);

                    // S'assurer que la valeur résiduelle ne soit pas négative
                    if (amortissement.ValeurResiduelleFinale < 0)
                    {
                        amortissement.ValeurResiduelleFinale = 0;
                    }

                    Console.WriteLine($"[INFO] Nouvelles valeurs - DureeMois: {amortissement.DureeMois}, ValeurResiduelle: {amortissement.ValeurResiduelleFinale}");

                    await _amortissementRepository.UpdateAsync(amortissement);
                    Console.WriteLine("[SUCCESS] Amortissement mis à jour avec succès !");

                    await SyncMoisAmortissements(amortissement.Id, newDuree, montantMensuel);
                }
                else
                {
                    Console.WriteLine("[ATTENTION] Aucun amortissement trouvé pour ce vélo !");

                    // Debug: Afficher tous les amortissements
                    foreach (var a in amortissements)
                    {
                        Console.WriteLine($"  - Amortissement ID {a.Id}: VeloId={a.VeloId}, IsActif={a.IsActif}");
                    }
                }
            }

            response.Success = true;
            response.Message = "Le contrat a été modifié avec succès.";
            response.Id = oldContrat.Id;

            Console.WriteLine("========== FIN UPDATE CONTRAT ==========");
            return response;
        }

        private async Task SyncMoisAmortissements(int amortissementId, int duree, decimal montantMensuel)
        {
            var existingMonths = await _moisAmortissementRepository.GetByAmortissementIdAsync(amortissementId);
            var existingByNumero = existingMonths.ToDictionary(m => m.NumeroMois, m => m);

            for (var mois = 1; mois <= duree; mois++)
            {
                if (existingByNumero.TryGetValue(mois, out var existing))
                {
                    if (existing.Montant != montantMensuel)
                    {
                        existing.Montant = montantMensuel;
                        existing.IsActif = true;
                        await _moisAmortissementRepository.UpdateAsync(existing);
                    }
                    continue;
                }

                var moisAmortissement = new MoisAmortissement
                {
                    AmortissementId = amortissementId,
                    NumeroMois = mois,
                    Montant = montantMensuel,
                    IsActif = true,
                };

                await _moisAmortissementRepository.CreateAsync(moisAmortissement);
            }

            foreach (var extra in existingMonths.Where(m => m.NumeroMois > duree))
            {
                await _moisAmortissementRepository.DeleteAsync(extra.Id);
            }
        }
    }
}
