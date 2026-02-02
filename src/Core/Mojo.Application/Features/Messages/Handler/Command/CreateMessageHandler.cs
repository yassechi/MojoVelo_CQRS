using Mojo.Application.DTOs.EntitiesDto.Message.Validators;

namespace Mojo.Application.Features.Messages.Handler.Command
{
    public class CreateMessageHandler : IRequestHandler<CreateMessageCommand, BaseResponse>
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IDiscussionRepository _discussionRepository;

        public CreateMessageHandler(
            IMessageRepository repository,
            IMapper mapper,
            IUserRepository userRepository,
            IDiscussionRepository discussionRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _discussionRepository = discussionRepository;
        }

        public async Task<BaseResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var validator = new MessageValidator(_userRepository, _discussionRepository);
            var validationResult = await validator.ValidateAsync(request.dto, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la création du message : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var message = _mapper.Map<Message>(request.dto);
            await _repository.CreateAsync(message);

            response.Success = true;
            response.Message = "Le message a été créé avec succès.";
            response.Id = message.Id;
            return response;
        }
    }
}