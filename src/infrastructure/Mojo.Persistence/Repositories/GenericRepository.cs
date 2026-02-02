using Microsoft.EntityFrameworkCore;
using Mojo.Application.Exceptions;
using Mojo.Application.Persistance.Contracts;
using Mojo.Persistence.DatabaseContext;

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
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(typeof(T).Name, id);
            }

            db.Set<T>().Remove(entity);
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

        public async Task<T?> GetByIdAsync(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            db.Set<T>().Update(entity);
            await SaveChangesAsync();
        }
    }
}