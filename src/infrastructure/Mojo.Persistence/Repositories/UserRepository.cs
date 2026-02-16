using Microsoft.EntityFrameworkCore;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;
using Mojo.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly MDbContext _db;

        public UserRepository(MDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task DeleteByStringId(string id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserByStringId(string id)
        {
            var user = await _db.Users.FindAsync(id);
            return user;
        }

        public async Task<bool> UserExists(string id)
        {
            var user = await _db.Users.FindAsync(id);
            return user != null;
        }

        public async Task<List<User>> GetByOrganisationIdAsync(int organisationId)
        {
            return await _db.Users
                .Where(u => u.OrganisationId == organisationId && u.IsActif)
                .ToListAsync();
        }
    }
}
