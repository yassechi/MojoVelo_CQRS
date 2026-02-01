using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Organisations.Handler.Command
{
    public class DeleteOrganisationHandler : IRequestHandler<DeleteOrganisationCommand, BaseResponse>
    {
        private readonly IOrganisationRepository repository;
        private readonly IMapper mapper;

        public DeleteOrganisationHandler(IOrganisationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteOrganisationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var organisation = await repository.GetByIdAsync(request.Id);
            if (organisation is null)
            {
                response.Succes = false;
                response.Message = $"L'organisation avec Id: {request.Id} n'existe pas !";
                return response;
            }

            await repository.DeleteAsync(request.Id);

            response.Succes = true;
            response.Message = $"L'organisation avec Id: {request.Id} est supprimé !";


            return response;
        }
    }
}
