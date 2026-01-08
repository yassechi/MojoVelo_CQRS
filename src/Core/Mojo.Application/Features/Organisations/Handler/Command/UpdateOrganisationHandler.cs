
namespace Mojo.Application.Features.Organisations.Handler.Command
{
    internal class UpdateOrganisationHandler : IRequestHandler<UpdateOrganisationCommand, Unit>
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
            var oldOrganisation = await repository.GetByIdAsync(request.dto.Id);
            var updatedOrganisation = mapper.Map(request.dto, oldOrganisation);
            await repository.UpadteAsync(updatedOrganisation);
            return Unit.Value;
        }
    }
}
