using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Discussions.Handler.Command
{
    public class DeleteDiscussionHandler : IRequestHandler<DeleteDiscussionCommand, BaseResponse>
    {
        private readonly IDiscussionRepository repository;
        private readonly IMapper mapper;

        public DeleteDiscussionHandler(IDiscussionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteDiscussionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var discussion = await repository.GetByIdAsync(request.Id);
            if (discussion is null)
            {
                response.Succes = false;
                response.Message = $"La discussion avec Id: {request.Id} n'existe pas !";
                return response;
            }

            await repository.DeleteAsync(request.Id);

            response.Succes = true;
            response.Message = $"La discussion avec Id: {request.Id} est supprimé !";

            return response;
        }
    }
}
