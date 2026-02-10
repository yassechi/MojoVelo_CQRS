using Microsoft.EntityFrameworkCore;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;
using Mojo.Persistence.DatabaseContext;

namespace Mojo.Persistence.Repositories
{
    public class DocumentRepository : GenericRepository<Documents>, IDocumentRepository
    {
        private readonly MDbContext _context;

        public DocumentRepository(MDbContext db) : base(db)
        {
            _context = db;
        }

        public async Task<List<Documents>> GetByContratIdAsync(int contratId)
        {
            return await _context.Set<Documents>()
                .Where(d => d.ContratId == contratId && d.IsActif)
                .ToListAsync();
        }
    }
}