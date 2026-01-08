namespace Mojo.Application.Persistance.Contracts
{
    public interface IVeloRepository : IGenericRepository<Velo>
    {
        Task<bool> NumeroSerieExists(string numeroSerie, int id);
    }
}