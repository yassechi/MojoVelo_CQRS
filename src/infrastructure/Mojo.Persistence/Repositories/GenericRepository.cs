global using Mojo.Application.Persistance.Contracts;
global using Mojo.Persistence.DatabaseContext;
using System.Data.Entity;

namespace Mojo.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MDbContext db;

        public GenericRepository(MDbContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(T entity)
        {
            await db.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            db.Set<T>().Remove(await GetByIdAsync(id));
            await SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entry = await GetByIdAsync(id);
            return entry != null;  
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await db.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
           return await db.Set<T>().FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public async Task UpadteAsync(T entity)
        {
            db.Entry(entity).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}
