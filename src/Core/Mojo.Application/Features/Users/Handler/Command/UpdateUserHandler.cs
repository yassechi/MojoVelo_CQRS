using Mojo.Application.DTOs.EntitiesDto.User.Validators;

namespace Mojo.Application.Features.Users.Handler.Command
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, BaseResponse>
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

        public async Task<BaseResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var validator = new UserValidator(_organisationRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la modification de l'utilisateur : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var oldUser = await _repository.GetUserByStringId(request.dto.Id);

            if (oldUser == null)
            {
                response.Succes = false;
                response.Message = "Echec de la modification de l'utilisateur.";
                response.Errors.Add($"Aucun utilisateur trouvé avec l'Id {request.dto.Id}.");
                return response;
            }

            _mapper.Map(request.dto, oldUser);
            await _repository.UpadteAsync(oldUser);

            response.Succes = true;
            response.Message = "L'utilisateur a été modifié avec succès.";
            response.StrId = oldUser.Id;

            return response;
        }
    }
}