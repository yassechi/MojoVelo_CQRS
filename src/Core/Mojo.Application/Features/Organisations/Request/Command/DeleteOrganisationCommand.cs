namespace Mojo.Application.Features.Organisations.Request.Command
{
    internal class DeleteOrganisationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
