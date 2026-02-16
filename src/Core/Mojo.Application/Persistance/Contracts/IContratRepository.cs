namespace Mojo.Application.Persistance.Contracts
{
    public interface IContratRepository : IGenericRepository<Contrat>
    {
        Task<List<Contrat>> GetByUserIdAsync(string userId);
        Task<List<Contrat>> GetByOrganisationIdAsync(int organisationId);
    }
}
