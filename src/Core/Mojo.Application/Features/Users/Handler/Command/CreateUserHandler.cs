using Mojo.Application.DTOs.EntitiesDto.User.Validators;
using Mojo.Application.Model;
using Mojo.Application.Persistance.Emails;

namespace Mojo.Application.Features.Users.Handler.Command
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IEmailSender _emailSender;

        public CreateUserHandler(IUserRepository repository, IMapper mapper, IOrganisationRepository organisationRepository, IEmailSender emailSender)
        {
            _repository = repository;
            _mapper = mapper;
            _organisationRepository = organisationRepository;
            _emailSender = emailSender;
        }

        public async Task<BaseResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var validator = new UserValidator(_organisationRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la création de l'utilisateur : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var user = _mapper.Map<User>(request.dto);
            await _repository.CreateAsync(user);

            try
            {
                var email = new EmailMessage
                {
                    To = user.Email,
                    Subject = "Bienvenue sur MojoVelo",
                    Body = $"Bonjour {user.FirstName} {user.LastName},\n\nVotre compte a été créé avec succès.\n\nCordialement,\nL'équipe MojoVelo"
                };
                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                response.Errors.Add($"Avertissement : L'utilisateur a été créé mais l'email de bienvenue n'a pas pu être envoyé. Erreur : {ex.Message}");
            }

            response.Succes = true;
            response.Message = "L'utilisateur a été créé avec succès.";
            response.StrId = user.Id;

            return response;
        }
    }
}