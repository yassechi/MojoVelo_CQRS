using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Contrats.Handler.Command
{
    public class DeleteContratHandler : IRequestHandler<DeleteContratCommand, BaseResponse>
    {
        private readonly IContratRepository repository;
        private readonly IMapper mapper;

        public DeleteContratHandler(IContratRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteContratCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var contrat = await repository.GetByIdAsync(request.Id);

            if (contrat is null)
            {
                response.Succes = false;
                response.Message = $"le contrat avec Id: {request.Id} n'existe pas !";
                return response;
            }
            await repository.DeleteAsync(request.Id);
            response.Succes = true;
            response.Message = $"l'amortissement avec Id: {request.Id} est supprimé !";


            return response;
        }
    }
}
