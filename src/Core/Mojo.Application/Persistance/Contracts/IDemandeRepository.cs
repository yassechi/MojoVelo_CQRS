using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Persistance.Contracts
{
    public interface IDemandeRepository : IGenericRepository<Demande>
    {
        Task<List<Demande>> GetByUserIdAsync(string userId);
        Task<List<Demande>> GetByOrganisationIdAsync(int organisationId);
    }
}
