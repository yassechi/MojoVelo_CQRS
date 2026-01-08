namespace Mojo.Application.Features.Discussion.Handler.Command
{
    internal class UpdateDiscussionHandler : IRequestHandler<UpdateDiscussionCommand, Unit>
    {
        private readonly IDiscussionRepository repository;
        private readonly IMapper mapper;

        public UpdateDiscussionHandler(IDiscussionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDiscussionCommand request, CancellationToken cancellationToken)
        {
            var oldDiscussion = await repository.GetByIdAsync(request.dto.Id);
            var updatedDuiscussion = mapper.Map(request.dto, oldDiscussion);
            await repository.UpadteAsync(updatedDuiscussion);
            return Unit.Value;
        }
    }
}
