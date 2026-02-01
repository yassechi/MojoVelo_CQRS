namespace Mojo.Application.Features.Organisations.Request.Command
{
    public class DeleteOrganisationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
