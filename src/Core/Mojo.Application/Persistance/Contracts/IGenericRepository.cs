namespace Mojo.Application.Persistance.Contracts
{
    public  interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T?> UpadteAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
        Task<bool> Exists(int id);
    }
}
