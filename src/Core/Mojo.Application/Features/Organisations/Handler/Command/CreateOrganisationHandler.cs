using Mojo.Application.DTOs.EntitiesDto.Organisation.Validators;

namespace Mojo.Application.Features.Organisations.Handler.Command
{
    public class CreateOrganisationHandler : IRequestHandler<CreateOrganisationCommand, BaseResponse>
    {
        private readonly IOrganisationRepository repository;
        private readonly IMapper mapper;

        public CreateOrganisationHandler(IOrganisationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new OrganisationValidator();
            var res = await validator.ValidateAsync(request.dto);
            if (!res.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la création de l'organisation !";
                response.Errors = res.Errors.Select(e => e.ErrorMessage).ToList();
            }

            response.Succes = true;
            response.Message = "Création de l'organisation avec succès..";
            response.Id = request.dto.Id;
        
            var organisation = mapper.Map<Organisation>(request.dto);
            await repository.CreateAsync(organisation);
            return response;
        }
    }
}
