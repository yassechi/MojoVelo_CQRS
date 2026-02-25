using Mojo.Application.DTOs.EntitiesDto.OrganisationLogo.Validators;
using Mojo.Application.Features.OrganisationLogos.Request.Command;

namespace Mojo.Application.Features.OrganisationLogos.Handler.Command
{
    public class CreateOrganisationLogoHandler : IRequestHandler<CreateOrganisationLogoCommand, BaseResponse>
    {
        private readonly IOrganisationLogoRepository _repository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IMapper _mapper;

        public CreateOrganisationLogoHandler(
            IOrganisationLogoRepository repository,
            IOrganisationRepository organisationRepository,
            IMapper mapper)
        {
            _repository = repository;
            _organisationRepository = organisationRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(CreateOrganisationLogoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new OrganisationLogoValidator(_organisationRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Organisation logo creation failed: validation errors.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            if (request.dto.IsActif)
            {
                await _repository.DeactivateOtherLogosAsync(request.dto.OrganisationId);
            }

            var logo = _mapper.Map<OrganisationLogo>(request.dto);
            await _repository.CreateAsync(logo);

            response.Success = true;
            response.Message = "Organisation logo created successfully.";
            response.Id = logo.Id;
            return response;
        }
    }
}
