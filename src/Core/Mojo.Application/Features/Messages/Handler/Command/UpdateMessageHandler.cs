
namespace Mojo.Application.Features.Messages.Handler.Command
{
    public class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand, Unit>
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IDiscussionRepository _discussionRepository;

        public UpdateMessageHandler(
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

        public async Task<Unit> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var validator = new MessageValidator(_userRepository, _discussionRepository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);
            if (res.IsValid == false) throw new Exceptions.ValidationException(res);

            var oldMessage = await _repository.GetByIdAsync(request.dto.Id);
            if (oldMessage == null) throw new Exception("Message introuvable.");

            _mapper.Map(request.dto, oldMessage);

            await _repository.UpadteAsync(oldMessage);

            return Unit.Value;
        }
    }
}