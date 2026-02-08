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
            var organisation = await _repository.GetByIdAsync(request.Id);

            if (organisation == null)
            {
                response.Success = false;
                response.Message = "Echec de la désactivation de l'organisation.";
                response.Errors.Add($"Aucune organisation trouvée avec l'Id {request.Id}.");
                return response;
            }

            // Au lieu de supprimer, on désactive l'organisation
            organisation.IsActif = false;
            await _repository.UpdateAsync(organisation);

            response.Success = true;
            response.Message = "L'organisation a été désactivée avec succès.";
            response.Id = request.Id;
            return response;
        }
    }
}