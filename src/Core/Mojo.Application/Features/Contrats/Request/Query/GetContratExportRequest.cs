namespace Mojo.Application.Features.Contrats.Request.Query
{
    public class GetContratExportRequest : IRequest<string>
    {
        public string? Type { get; set; }
        public string? Search { get; set; }
        public bool? EndingSoon { get; set; }
        public bool? WithIncidents { get; set; }
        public int? OrganisationId { get; set; }
        public string? UserId { get; set; }
    }
}
