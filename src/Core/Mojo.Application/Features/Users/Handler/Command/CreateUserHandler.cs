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
        private readonly IEmailSender emailSender;

        public CreateUserHandler(IUserRepository repository, IMapper mapper, IOrganisationRepository organisationRepository, IEmailSender emailSender)
        {
            _repository = repository;
            _mapper = mapper;
            _organisationRepository = organisationRepository;
            this.emailSender = emailSender;
        }

        public async Task<BaseResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new UserValidator(_organisationRepository);
            var res = await validator.ValidateAsync(request.dto, cancellationToken);

            if (!res.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la création de l'utilsateur !";
                response.Errors = res.Errors.Select(e => e.ErrorMessage).ToList();
            }

            response.Succes = true;
            response.Message = "Création de l'utilisateur avec succès..";
            response.StrId = request.dto.Id;

            var user = _mapper.Map<User>(request.dto);

            await _repository.CreateAsync(user);

            // Send Email New User
            try
            {
                var email = new EmailMessage
                {
                    To = "yassechi@gmail.com",
                    Subject = "test send mail Succes !",
                    Body = "the User is benn added succefuly this is a body from message ..."
                };
                await emailSender.SendEmail(email);


            } catch (Exception)
            {
                throw;
            }

            return response;
        }
    }
}