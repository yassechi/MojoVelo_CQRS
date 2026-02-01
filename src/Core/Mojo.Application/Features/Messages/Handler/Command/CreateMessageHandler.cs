using AutoMapper;

namespace Mojo.Application.Features.Messages.Handler.Command
{
    public class CreateMessageHandle : IRequestHandler<CreateMessageCommand, BaseResponse>
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

        public async Task<BaseResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new MessageValidator(_userRepository, _discussionRepository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);

            if (!res.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la création du message !";
                response.Errors = res.Errors.Select(e => e.ErrorMessage).ToList();
            }

            response.Succes = true;
            response.Message = "Crétion du message avec succès..";
            response.Id = request.dto.Id;

            var message = _mapper.Map<Message>(request.dto);
            message.DateEnvoi = DateTime.Now; 

            await _repository.CreateAsync(message);

            return response;
        }
    }
}