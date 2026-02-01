using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Velos.Handler.Command
{
    public class DeleteVeloHandler : IRequestHandler<DeleteVeloCommand, BaseResponse>
    {
        private readonly IVeloRepository repository;
        private readonly IMapper mapper;

        public DeleteVeloHandler(IVeloRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteVeloCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var velo = await repository.GetByIdAsync(request.Id);
            if (velo is null)
            {
                response.Succes = false;
                response.Message = $"Le velo avec Id: {request.Id} n'existe pas !";
                return response;
            }

            await repository.DeleteAsync(request.Id);

            response.Succes = true;
            response.Message = $"Le velo avec Id: {request.Id} est supprimé !";


            return response;
        }
    }
}
