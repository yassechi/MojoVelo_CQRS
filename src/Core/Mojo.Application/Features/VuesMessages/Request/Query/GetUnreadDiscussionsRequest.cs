namespace Mojo.Application.Features.VuesMessages.Request.Query
{
    public class GetUnreadDiscussionsRequest : IRequest<List<int>>
    {
        public string UserId { get; set; } = string.Empty;
        public int Role { get; set; }
        public int? OrganisationId { get; set; }
    }
}
