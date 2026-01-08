using Mojo.Application.DTOs.EntitiesDto.Organisation.Validators;

namespace Mojo.Application.Features.Organisations.Handler.Command
{
    public class UpdateOrganisationHandler : IRequestHandler<UpdateOrganisationCommand, Unit>
    {
        private readonly IOrganisationRepository repository;
        private readonly IMapper mapper;

        public UpdateOrganisationHandler(IOrganisationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateOrganisationCommand request, CancellationToken cancellationToken)
        {
            var validator = new OrganisationValidator();
            var res = await validator.ValidateAsync(request.dto);
            if (res.IsValid == false) throw new Exceptions.ValidationException(res);


            var oldOrganisation = await repository.GetByIdAsync(request.dto.Id);
            var updatedOrganisation = mapper.Map(request.dto, oldOrganisation);
            await repository.UpadteAsync(updatedOrganisation);
            return Unit.Value;
        }
    }
}
