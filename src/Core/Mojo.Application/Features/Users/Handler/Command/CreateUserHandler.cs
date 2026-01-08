using Mojo.Application.DTOs.EntitiesDto.User.Validators;

namespace Mojo.Application.Features.Users.Handler.Command
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IOrganisationRepository _organisationRepository;

        public CreateUserHandler(IUserRepository repository, IMapper mapper, IOrganisationRepository organisationRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _organisationRepository = organisationRepository;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new UserValidator(_organisationRepository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);

            if (!res.IsValid)
            {
                throw new Exception("La validation de l'utilisateur a échoué.");
            }

            var user = _mapper.Map<User>(request.dto);

            await _repository.CreateAsync(user);

            return Unit.Value;
        }
    }
}