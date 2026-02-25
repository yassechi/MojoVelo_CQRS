using Microsoft.EntityFrameworkCore;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;
using Mojo.Persistence.DatabaseContext;

namespace Mojo.Persistence.Repositories
{
    public class OrganisationLogoRepository : GenericRepository<OrganisationLogo>, IOrganisationLogoRepository
    {
        private readonly MDbContext _context;

        public OrganisationLogoRepository(MDbContext db) : base(db)
        {
            _context = db;
        }

        public async Task<List<OrganisationLogo>> GetByOrganisationIdAsync(int organisationId)
        {
            return await _context.Set<OrganisationLogo>()
                .Where(l => l.OrganisationId == organisationId)
                .OrderByDescending(l => l.IsActif)
                .ThenByDescending(l => l.CreatedDate)
                .ToListAsync();
        }

        public async Task DeactivateOtherLogosAsync(int organisationId, int? keepLogoId = null)
        {
            var logos = await _context.Set<OrganisationLogo>()
                .Where(l => l.OrganisationId == organisationId && l.IsActif)
                .ToListAsync();

            foreach (var logo in logos)
            {
                if (keepLogoId.HasValue && logo.Id == keepLogoId.Value)
                {
                    continue;
                }
                logo.IsActif = false;
            }

            await _context.SaveChangesAsync();
        }
    }
}
