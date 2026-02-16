using Microsoft.EntityFrameworkCore;
using Mojo.Application.Persistance.Contracts;
using Mojo.Persistence.DatabaseContext;

namespace Mojo.Persistence.Repositories
{
    public class MoisAmortissementRepository : GenericRepository<MoisAmortissement>, IMoisAmortissementRepository
    {
        private readonly MDbContext _context;

        public MoisAmortissementRepository(MDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<MoisAmortissement>> GetByAmortissementIdAsync(int amortissementId)
        {
            return await _context.MoisAmortissements
                .AsNoTracking()
                .Where(m => m.AmortissementId == amortissementId)
                .OrderBy(m => m.NumeroMois)
                .ToListAsync();
        }

        public async Task<bool> ExistsForMonthAsync(int amortissementId, int numeroMois, int? excludeId = null)
        {
            var query = _context.MoisAmortissements.AsNoTracking()
                .Where(m => m.AmortissementId == amortissementId && m.NumeroMois == numeroMois);
            if (excludeId.HasValue)
            {
                query = query.Where(m => m.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }
    }
}
