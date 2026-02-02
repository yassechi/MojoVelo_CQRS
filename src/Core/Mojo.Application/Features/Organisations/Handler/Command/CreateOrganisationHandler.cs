using Mojo.Application.DTOs.EntitiesDto.Organisation.Validators;

namespace Mojo.Application.Features.Organisations.Handler.Command
{
    public class CreateOrganisationHandler : IRequestHandler<CreateOrganisationCommand, BaseResponse>
    {
        private readonly IOrganisationRepository _repository;
        private readonly IMapper _mapper;

        public CreateOrganisationHandler(IOrganisationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new OrganisationValidator();
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la création de l'organisation : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var organisation = _mapper.Map<Organisation>(request.dto);
            await _repository.CreateAsync(organisation);

            response.Success = true;
            response.Message = "L'organisation a été créée avec succès.";
            response.Id = organisation.Id;
            return response;
        }
    }
}