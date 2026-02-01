using Mojo.Application.DTOs.EntitiesDto.User.Validators;

namespace Mojo.Application.Features.Users.Handler.Command
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IOrganisationRepository _organisationRepository;

        public UpdateUserHandler(IUserRepository repository, IMapper mapper, IOrganisationRepository organisationRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _organisationRepository = organisationRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new UserValidator(_organisationRepository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);
            if (res.IsValid == false) throw new Exceptions.ValidationException(res);

            var oldUser = await _repository.GetUserByStringId(request.dto.Id);
            if (oldUser == null) throw new Exception("Utilisateur introuvable.");

            _mapper.Map(request.dto, oldUser);

            await _repository.UpadteAsync(oldUser);

            return Unit.Value;
        }
    }
}