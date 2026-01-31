using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly MDbContext db;

        public UserRepository(MDbContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<User> GetUserByStringId(string id)
        {
            var user = await db.FindAsync<User>(id);
            return user;
        }

        public async Task<bool> UserExists(string id)
        {
            var user = await db.FindAsync<User>(id);
            return user != null;

        }
    }
}
