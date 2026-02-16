using Microsoft.EntityFrameworkCore;
using Mojo.Application.Persistance.Contracts;
using Mojo.Persistence.DatabaseContext;

namespace Mojo.Persistence.Repositories
{
    public class InterventionRepository : GenericRepository<Intervention>, IInterventionRepository
    {
        private readonly MDbContext _db;

        public InterventionRepository(MDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Intervention>> GetByVeloIdAsync(int veloId)
        {
            return await _db.Set<Intervention>()
                .Where(intervention => intervention.VeloId == veloId && intervention.IsActif)
                .OrderByDescending(intervention => intervention.DateIntervention)
                .ToListAsync();
        }
    }
}
