using Mojo.Application.DTOs.Dashboard;

namespace Mojo.Application.Features.Dashboard.Request.Query
{
    public class GetManagerDashboardRequest : IRequest<ManagerDashboardDto>
    {
        public int OrganisationId { get; set; }
    }
}
