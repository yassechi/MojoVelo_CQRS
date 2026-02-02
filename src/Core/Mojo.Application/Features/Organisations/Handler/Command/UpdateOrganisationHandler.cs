using Mojo.Application.DTOs.EntitiesDto.Organisation.Validators;

namespace Mojo.Application.Features.Organisations.Handler.Command
{
    public class UpdateOrganisationHandler : IRequestHandler<UpdateOrganisationCommand, BaseResponse>
    {
        private readonly IOrganisationRepository _repository;
        private readonly IMapper _mapper;

        public UpdateOrganisationHandler(IOrganisationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(UpdateOrganisationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new OrganisationValidator();
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la modification de l'organisation : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var oldOrganisation = await _repository.GetByIdAsync(request.dto.Id);
            if (oldOrganisation == null)
            {
                response.Success = false;
                response.Message = "Echec de la modification de l'organisation.";
                response.Errors.Add($"Aucune organisation trouvée avec l'Id {request.dto.Id}.");
                return response;
            }

            _mapper.Map(request.dto, oldOrganisation);
            await _repository.UpdateAsync(oldOrganisation);

            response.Success = true;
            response.Message = "L'organisation a été modifiée avec succès.";
            response.Id = oldOrganisation.Id;
            return response;
        }
    }
}