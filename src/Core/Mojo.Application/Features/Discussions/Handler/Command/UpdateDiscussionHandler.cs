using Mojo.Application.DTOs.EntitiesDto.Discussion.Validators;

namespace Mojo.Application.Features.Discussion.Handler.Command
{
    public class UpdateDiscussionHandler : IRequestHandler<UpdateDiscussionCommand, Unit>
    {
        private readonly IDiscussionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UpdateDiscussionHandler(IDiscussionRepository repository, IMapper mapper, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateDiscussionCommand request, CancellationToken cancellationToken)
        {
            var validator = new DiscussionValidator(_userRepository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);
            if (!res.IsValid) throw new Exception("La validation de la discussion a échoué.");

            var oldDiscussion = await _repository.GetByIdAsync(request.dto.Id);
            if (oldDiscussion == null) throw new Exception("Discussion introuvable.");

            _mapper.Map(request.dto, oldDiscussion);

            await _repository.UpadteAsync(oldDiscussion);

            return Unit.Value;
        }
    }
}