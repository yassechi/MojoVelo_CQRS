using Mojo.Application.DTOs.EntitiesDto.Contrat;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Contrats.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Contrats.Handler.Query
{
    public class GetContratDetailViewHandler : IRequestHandler<GetContratDetailViewRequest, ContratDetailDto>
    {
        private readonly IContratRepository _contratRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVeloRepository _veloRepository;

        public GetContratDetailViewHandler(
            IContratRepository contratRepository,
            IUserRepository userRepository,
            IVeloRepository veloRepository)
        {
            _contratRepository = contratRepository;
            _userRepository = userRepository;
            _veloRepository = veloRepository;
        }

        public async Task<ContratDetailDto> Handle(GetContratDetailViewRequest request, CancellationToken cancellationToken)
        {
            var contrat = await _contratRepository.GetByIdAsync(request.Id);

            if (contrat == null || !contrat.IsActif)
            {
                throw new NotFoundException(nameof(Contrat), request.Id);
            }

            var beneficiaire = await _userRepository.GetUserByStringId(contrat.BeneficiaireId);
            var responsableRh = await _userRepository.GetUserByStringId(contrat.UserRhId);
            var velo = await _veloRepository.GetByIdAsync(contrat.VeloId);

            var beneficiaireName = beneficiaire == null
                ? contrat.BeneficiaireId
                : $"{beneficiaire.FirstName} {beneficiaire.LastName}".Trim();

            var responsableName = responsableRh == null
                ? contrat.UserRhId
                : $"{responsableRh.FirstName} {responsableRh.LastName}".Trim();

            return new ContratDetailDto
            {
                Id = contrat.Id,
                Ref = contrat.Ref ?? string.Empty,
                VeloId = contrat.VeloId,
                VeloMarque = velo?.Marque ?? string.Empty,
                VeloModele = velo?.Modele ?? string.Empty,
                VeloNumeroSerie = velo?.NumeroSerie,
                BeneficiaireId = contrat.BeneficiaireId,
                BeneficiaireName = beneficiaireName,
                UserRhId = contrat.UserRhId,
                UserRhName = responsableName,
                DateDebut = contrat.DateDebut,
                DateFin = contrat.DateFin,
                Duree = contrat.Duree,
                LoyerMensuelHT = contrat.LoyerMensuelHT,
                StatutContrat = contrat.StatutContrat
            };
        }
    }
}
