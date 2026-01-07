using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Mojo.Application.Persistance.Contracts
{
    public  interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T?> UpadteAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
