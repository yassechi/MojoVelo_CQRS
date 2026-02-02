using Mojo.Application.Persistance.Contracts;
using Mojo.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Persistence.Repositories
{
    public class ContratRepository : GenericRepository<Contrat>, IContratRepository
    {
        public ContratRepository(MDbContext db) : base(db)
        {
            
        }
    }
}
