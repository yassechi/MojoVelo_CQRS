namespace Mojo.Application.Features.Organisations.Request.Query
{
    internal class GetOrganisationDetailsRequest : IRequest<OrganisationDto>
    {
        public int Id { get; set; }
    }
}
