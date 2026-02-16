using Microsoft.EntityFrameworkCore;
using Mojo.Application.Persistance.Contracts;
using Mojo.Persistence.DatabaseContext;

namespace Mojo.Persistence.Repositories
{
    public class AmortissementRepository : GenericRepository<Amortissement>, IAmortissementRepository
    {
        private readonly MDbContext _db;

        public AmortissementRepository(MDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Amortissement>> GetByVeloIdAsync(int veloId)
        {
            return await _db.Set<Amortissement>()
                .Where(amortissement => amortissement.VeloId == veloId && amortissement.IsActif)
                .OrderByDescending(amortissement => amortissement.DateDebut)
                .ToListAsync();
        }
    }
}
