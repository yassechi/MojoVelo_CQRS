namespace Mojo.Application.Persistance.Contracts
{
    public interface IVuesMessageRepository : IGenericRepository<VuesMessage>
    {
        Task<int> GetUnreadCountAsync(string userId, int role, int? organisationId);
        Task<List<int>> GetUnreadDiscussionIdsAsync(string userId, int role, int? organisationId);
        Task<int> MarkMessagesAsReadAsync(string userId, IEnumerable<int> messageIds);
    }
}
