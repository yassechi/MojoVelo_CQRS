using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Interventions.Handler.Command
{
    public class DeleteInterventionHandler : IRequestHandler<DeleteInterventionCommand, BaseResponse>
    {
        private readonly IInterventionRepository repository;
        private readonly IMapper mapper;

        public DeleteInterventionHandler(IInterventionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteInterventionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var intervention = await repository.GetByIdAsync(request.Id);

            if (intervention is null)
            {
                response.Succes = false;
                response.Message = $"L'intervention avec Id: {request.Id} n'existe pas !";
                return response;
            }
            await repository.DeleteAsync(request.Id);

            response.Succes = true;
            response.Message = $"L'intervention avec Id: {request.Id} est supprimé !";

            return response;
        }
    }
}
