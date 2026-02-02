using Mojo.Application.Persistance.Contracts;
using Mojo.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Persistence.Repositories
{
    public class VeloRepository : GenericRepository<Velo>, IVeloRepository
    {
        private readonly MDbContext db;

        public VeloRepository(MDbContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<bool> NumeroSerieExists(string numeroSerie, int id)
        {
            var velo = await db.FindAsync<Velo>(numeroSerie);
            return velo != null;
        }
    }
}
 