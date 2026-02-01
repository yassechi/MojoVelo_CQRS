using Mojo.Application.DTOs.EntitiesDto.Message.Validators;

namespace Mojo.Application.Features.Messages.Handler.Command
{
    public class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand, BaseResponse>
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

        public async Task<BaseResponse> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var validator = new MessageValidator(_userRepository, _discussionRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la modification du message : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var oldMessage = await _repository.GetByIdAsync(request.dto.Id);

            if (oldMessage == null)
            {
                response.Succes = false;
                response.Message = "Echec de la modification du message.";
                response.Errors.Add($"Aucun message trouvé avec l'Id {request.dto.Id}.");
                return response;
            }

            _mapper.Map(request.dto, oldMessage);
            await _repository.UpadteAsync(oldMessage);

            response.Succes = true;
            response.Message = "Le message a été modifié avec succès.";
            response.Id = oldMessage.Id;

            return response;
        }
    }
}