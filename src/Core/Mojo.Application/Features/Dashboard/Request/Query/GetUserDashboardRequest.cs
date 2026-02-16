using Mojo.Application.DTOs.Dashboard;

namespace Mojo.Application.Features.Dashboard.Request.Query
{
    public class GetUserDashboardRequest : IRequest<UserDashboardDto>
    {
        public string UserId { get; set; } = null!;
    }
}
