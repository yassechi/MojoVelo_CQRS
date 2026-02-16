namespace Mojo.Application.Persistance.Contracts
{
    public interface IAmortissementRepository : IGenericRepository<Amortissement>
    {
        Task<List<Amortissement>> GetByVeloIdAsync(int veloId);
    }
}
