using Mojo.Application.Features.OrganisationLogos.Request.Command;

namespace Mojo.Application.Features.OrganisationLogos.Handler.Command
{
    public class DeleteOrganisationLogoHandler : IRequestHandler<DeleteOrganisationLogoCommand, BaseResponse>
    {
        private readonly IOrganisationLogoRepository _repository;

        public DeleteOrganisationLogoHandler(IOrganisationLogoRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse> Handle(DeleteOrganisationLogoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var logo = await _repository.GetByIdAsync(request.Id);

            if (logo == null)
            {
                response.Success = false;
                response.Message = "Organisation logo not found.";
                response.Errors.Add($"No logo found with Id {request.Id}.");
                return response;
            }

            logo.IsActif = false;
            await _repository.UpdateAsync(logo);

            response.Success = true;
            response.Message = "Organisation logo deactivated successfully.";
            response.Id = request.Id;
            return response;
        }
    }
}
