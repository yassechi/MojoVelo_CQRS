using Mojo.Application.DTOs.EntitiesDto.Organisation.Validators;

namespace Mojo.Application.Features.Organisations.Handler.Command
{
    public class CreateOrganisationHandler : IRequestHandler<CreateOrganisationCommand, Unit>
    {
        private readonly IOrganisationRepository repository;
        private readonly IMapper mapper;

        public CreateOrganisationHandler(IOrganisationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
        {
            var validator = new OrganisationValidator();
            var res = await validator.ValidateAsync(request.dto);
            if (!res.IsValid) throw new Exception();

            var organisation = mapper.Map<Organisation>(request.dto);
            await repository.CreateAsync(organisation);
            return Unit.Value;
        }
    }
}
