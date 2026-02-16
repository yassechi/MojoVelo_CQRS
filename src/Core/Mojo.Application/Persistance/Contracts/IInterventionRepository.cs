namespace Mojo.Application.Persistance.Contracts
{
    public interface IInterventionRepository : IGenericRepository<Intervention>
    {
        Task<List<Intervention>> GetByVeloIdAsync(int veloId);
    }
}
