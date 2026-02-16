using Mojo.Application.DTOs.EntitiesDto.User;
using Mojo.Application.Features.Users.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Users.Handler.Query
{
    public class GetUsersByOrganisationHandler : IRequestHandler<GetUsersByOrganisationRequest, List<UserDto>>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetUsersByOrganisationHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUsersByOrganisationRequest request, CancellationToken cancellationToken)
        {
            if (request.OrganisationId <= 0)
            {
                return new List<UserDto>();
            }

            var users = await _repository.GetByOrganisationIdAsync(request.OrganisationId);
            if (request.Role.HasValue)
            {
                users = users.Where(u => u.Role == request.Role.Value).ToList();
            }

            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
