namespace Mojo.Application.DTOs.Dashboard
{
    public class AdminDashboardDto
    {
        public int PendingDemandes { get; set; }
        public int ActiveContrats { get; set; }
        public decimal BudgetTotal { get; set; }
        public List<ActivityFeedItemDto> ActivityFeed { get; set; } = new();
        public List<BikeTypeCountDto> BikeTypeCounts { get; set; } = new();
    }
}
