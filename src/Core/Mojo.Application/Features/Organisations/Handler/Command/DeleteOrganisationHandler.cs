using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Organisations.Handler.Command
{
    public class DeleteOrganisationHandler : IRequestHandler<DeleteOrganisationCommand, BaseResponse>
    {
        private readonly IOrganisationRepository _repository;
        private readonly IMapper _mapper;

        public DeleteOrganisationHandler(IOrganisationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteOrganisationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            // Vérifier si l'entité existe
            var organisation = await _repository.GetByIdAsync(request.Id);

            if (organisation == null)
            {
                response.Succes = false;
                response.Message = "Echec de la suppression de l'organisation.";
                response.Errors.Add($"Aucune organisation trouvée avec l'Id {request.Id}.");
                return response;
            }

            // Suppression
            await _repository.DeleteAsync(request.Id);

            // Succès
            response.Succes = true;
            response.Message = "L'organisation a été supprimée avec succès.";
            response.Id = request.Id;

            return response;
        }
    }
}