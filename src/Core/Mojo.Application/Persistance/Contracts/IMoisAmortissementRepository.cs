namespace Mojo.Application.Persistance.Contracts
{
    public interface IMoisAmortissementRepository : IGenericRepository<MoisAmortissement>
    {
        Task<List<MoisAmortissement>> GetByAmortissementIdAsync(int amortissementId);
        Task<bool> ExistsForMonthAsync(int amortissementId, int numeroMois, int? excludeId = null);
    }
}
