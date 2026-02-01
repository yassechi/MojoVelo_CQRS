using Mojo.Application.Features.Amortissments.Request.Command;

namespace Mojo.Application.Features.Amortissments.Handler.Command
{
    public class DeleteAmortissementHandler : IRequestHandler<DeleteAmortissementCommand, BaseResponse>
    {
        private readonly IAmortissementRepository repository;
        private readonly IMapper mapper;

        public  DeleteAmortissementHandler(IAmortissementRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<BaseResponse> Handle(DeleteAmortissementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var amortissement = await repository.GetByIdAsync(request.Id);

            if (amortissement is null)
            {
                response.Succes = false;
                response.Message = $"L'amortissement avec Id: {request.Id} n'existe pas !";
                return response; 
            }

            await repository.DeleteAsync(request.Id); // Supprimer d'abord

            response.Succes = true;
            response.Message = $"L'amortissement avec Id: {request.Id} est supprimé !";

            return response;
        }
    }
}
