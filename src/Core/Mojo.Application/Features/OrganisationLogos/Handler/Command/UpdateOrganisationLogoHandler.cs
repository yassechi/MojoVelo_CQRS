using Mojo.Application.DTOs.EntitiesDto.OrganisationLogo.Validators;
using Mojo.Application.Features.OrganisationLogos.Request.Command;

namespace Mojo.Application.Features.OrganisationLogos.Handler.Command
{
    public class UpdateOrganisationLogoHandler : IRequestHandler<UpdateOrganisationLogoCommand, BaseResponse>
    {
        private readonly IOrganisationLogoRepository _repository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IMapper _mapper;

        public UpdateOrganisationLogoHandler(
            IOrganisationLogoRepository repository,
            IOrganisationRepository organisationRepository,
            IMapper mapper)
        {
            _repository = repository;
            _organisationRepository = organisationRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(UpdateOrganisationLogoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new OrganisationLogoValidator(_organisationRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Organisation logo update failed: validation errors.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var oldLogo = await _repository.GetByIdAsync(request.dto.Id);
            if (oldLogo == null)
            {
                response.Success = false;
                response.Message = "Organisation logo not found.";
                response.Errors.Add($"No logo found with Id {request.dto.Id}.");
                return response;
            }

            _mapper.Map(request.dto, oldLogo);

            if (request.dto.IsActif)
            {
                await _repository.DeactivateOtherLogosAsync(request.dto.OrganisationId, oldLogo.Id);
            }

            await _repository.UpdateAsync(oldLogo);

            response.Success = true;
            response.Message = "Organisation logo updated successfully.";
            response.Id = oldLogo.Id;
            return response;
        }
    }
}
