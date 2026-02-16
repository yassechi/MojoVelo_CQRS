using Mojo.Application.DTOs.EntitiesDto.Contrat;
using Mojo.Application.DTOs.EntitiesDto.User;
using Mojo.Application.DTOs.EntitiesDto.Velo;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Contrats.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Contrats.Handler.Query
{
    public class GetContratEditDataHandler : IRequestHandler<GetContratEditDataRequest, ContratEditDataDto>
    {
        private readonly IContratRepository _contratRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVeloRepository _veloRepository;
        private readonly IMapper _mapper;

        public GetContratEditDataHandler(
            IContratRepository contratRepository,
            IUserRepository userRepository,
            IVeloRepository veloRepository,
            IMapper mapper)
        {
            _contratRepository = contratRepository;
            _userRepository = userRepository;
            _veloRepository = veloRepository;
            _mapper = mapper;
        }

        public async Task<ContratEditDataDto> Handle(GetContratEditDataRequest request, CancellationToken cancellationToken)
        {
            var contrat = await _contratRepository.GetByIdAsync(request.Id);

            if (contrat == null || !contrat.IsActif)
            {
                throw new NotFoundException(nameof(Contrat), request.Id);
            }

            var users = (await _userRepository.GetAllAsync())
                .Where(user => user.IsActif)
                .Select(user => new AdminUserListItemDto
                {
                    Id = user.Id,
                    UserName = user.UserName ?? string.Empty,
                    FirstName = user.FirstName ?? string.Empty,
                    LastName = user.LastName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    PhoneNumber = user.PhoneNumber ?? string.Empty,
                    Role = user.Role,
                    IsActif = user.IsActif,
                    OrganisationId = user.OrganisationId
                })
                .ToList();

            var velos = (await _veloRepository.GetAllAsync())
                .Where(velo => velo.IsActif)
                .ToList();

            return new ContratEditDataDto
            {
                Contrat = _mapper.Map<ContratDto>(contrat),
                Users = users,
                Velos = _mapper.Map<List<VeloDto>>(velos)
            };
        }
    }
}
