using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public UserRepository(MDbContext db, UserManager<User> userManager)
            : base(db)
        {
            this.db = db;
            _userManager = userManager;
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

        public async Task<bool> UserHasRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            return await _userManager.IsInRoleAsync(user, roleName);
        }
    }
}