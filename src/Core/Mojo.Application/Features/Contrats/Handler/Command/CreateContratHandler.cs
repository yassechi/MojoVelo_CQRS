using Mojo.Application.DTOs.EntitiesDto.Contrat.Validators;
using MediatR;
using AutoMapper;
using Mojo.Application.DTOs.Common;

namespace Mojo.Application.Features.Contrats.Handler.Command
{
    public class CreateContratHandler : IRequestHandler<CreateContratCommand, BaseResponse>
    {
        private readonly IContratRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAmortissementRepository _amortissementRepository;
        private readonly IMoisAmortissementRepository _moisAmortissementRepository;

        public CreateContratHandler(
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

        public async Task<BaseResponse> Handle(CreateContratCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new ContratValidator(_veloRepository, _userRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("default", "Create");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la création du contrat : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var contrat = _mapper.Map<Mojo.Domain.Entities.Contrat>(request.dto);
            await _repository.CreateAsync(contrat);

            var montantMensuel = request.dto.MontantAmortissementMensuel ?? request.dto.LoyerMensuelHT;
            if (montantMensuel > 0 && request.dto.Duree > 0)
            {
                var amortissements = await _amortissementRepository.GetByVeloIdAsync(request.dto.VeloId);
                var amortissement = amortissements.FirstOrDefault(a => a.IsActif) ?? amortissements.FirstOrDefault();

                var amortissementCreated = false;
                if (amortissement == null)
                {
                    var velo = await _veloRepository.GetByIdAsync(request.dto.VeloId);
                    var valeurInit = velo?.PrixAchat ?? 0m;
                    var residuel = valeurInit - (montantMensuel * request.dto.Duree);
                    if (residuel < 0)
                    {
                        residuel = 0;
                    }

                    amortissement = new Amortissement
                    {
                        DateDebut = request.dto.DateDebut,
                        DureeMois = request.dto.Duree,
                        ValeurInit = valeurInit,
                        ValeurResiduelleFinale = residuel,
                        VeloId = request.dto.VeloId,
                        IsActif = true,
                    };

                    await _amortissementRepository.CreateAsync(amortissement);
                    amortissementCreated = true;
                }

                var existingMonths = await _moisAmortissementRepository.GetByAmortissementIdAsync(amortissement.Id);
                var existingMonthNumbers = existingMonths.Select(m => m.NumeroMois).ToHashSet();

                if (existingMonths.Count == 0 && !amortissementCreated)
                {
                    var valeurInit = amortissement.ValeurInit;
                    if (valeurInit <= 0)
                    {
                        var velo = await _veloRepository.GetByIdAsync(request.dto.VeloId);
                        if (velo != null)
                        {
                            valeurInit = velo.PrixAchat;
                            amortissement.ValeurInit = valeurInit;
                        }
                    }

                    amortissement.DureeMois = request.dto.Duree;
                    var residuel = valeurInit - (montantMensuel * request.dto.Duree);
                    amortissement.ValeurResiduelleFinale = residuel < 0 ? 0 : residuel;
                    await _amortissementRepository.UpdateAsync(amortissement);
                }

                for (var mois = 1; mois <= request.dto.Duree; mois++)
                {
                    if (existingMonthNumbers.Contains(mois))
                    {
                        continue;
                    }

                    var moisAmortissement = new MoisAmortissement
                    {
                        AmortissementId = amortissement.Id,
                        NumeroMois = mois,
                        Montant = montantMensuel,
                        IsActif = true,
                    };

                    await _moisAmortissementRepository.CreateAsync(moisAmortissement);
                }
            }

            response.Success = true;
            response.Message = "Le contrat a été créé avec succès.";
            response.Id = contrat.Id;
            return response;
        }
    }
}
