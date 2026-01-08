using Mojo.Application.DTOs.EntitiesDto.Discussion.Validators;

namespace Mojo.Application.Features.Discussion.Handler.Command
{
    public class CreateDiscussionHandler : IRequestHandler<CreateDiscussionCommand, Unit>
    {
        private readonly IDiscussionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateDiscussionHandler(IDiscussionRepository repository, IMapper mapper, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateDiscussionCommand request, CancellationToken cancellationToken)
        {
            var validator = new DiscussionValidator(_userRepository);

            var res = await validator.ValidateAsync(request.dto, cancellationToken);

            if (!res.IsValid)
            {
                throw new Exception("Validation de la discussion échouée.");
            }

            var discussion = _mapper.Map<Mojo.Domain.Entities.Discussion>(request.dto);

            await _repository.CreateAsync(discussion);

            return Unit.Value;
        }
    }
}