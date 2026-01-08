
namespace Mojo.Application.Features.Organisations.Handler.Command
{
    internal class DeleteOrganisationHandler : IRequestHandler<DeleteOrganisationCommand, Unit>
    {
        private readonly IOrganisationRepository repository;
        private readonly IMapper mapper;

        public DeleteOrganisationHandler(IOrganisationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteOrganisationCommand request, CancellationToken cancellationToken)
        {
            var organisation = await repository.GetByIdAsync(request.Id);

            if (organisation != null)
            {
                await repository.DeleteAsync(request.Id);
            }

            return Unit.Value;
        }
    }
}
