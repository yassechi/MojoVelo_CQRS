namespace Mojo.Application.DTOs.Dashboard
{
    public class AdminDashboardDto
    {
        public int PendingDemandes { get; set; }
        public int ActiveContrats { get; set; }
        public int ExpiringContrats { get; set; }
        public List<ActivityFeedItemDto> ActivityFeed { get; set; } = new();
    }
}
