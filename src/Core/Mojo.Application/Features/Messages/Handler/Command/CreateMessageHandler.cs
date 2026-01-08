using Mojo.Application.DTOs.EntitiesDto.Message.Validators;

namespace Mojo.Application.Features.Messages.Handler.Command
{
    public class CreateMessageHandle : IRequestHandler<CreateMessageCommand, Unit>
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IDiscussionRepository _discussionRepository;

        public CreateMessageHandle(
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

        public async Task<Unit> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var validator = new MessageValidator(_userRepository, _discussionRepository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);

            if (!res.IsValid)
            {
                throw new Exception("La validation du message a échoué.");
            }

            var message = _mapper.Map<Message>(request.dto);

            await _repository.CreateAsync(message);

            return Unit.Value;
        }
    }
}