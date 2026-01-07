using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Mojo.Application.Persistance.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserById(string id);
    }
}