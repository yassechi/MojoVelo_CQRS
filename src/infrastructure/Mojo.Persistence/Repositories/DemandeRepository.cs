global using Mojo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Mojo.Application.Persistance.Contracts;
using Mojo.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Persistence.Repositories
{
    public class DemandeRepository : GenericRepository<Demande>, IDemandeRepository
    {
        private readonly MDbContext _db;

        public DemandeRepository(MDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Demande>> GetByUserIdAsync(string userId)
        {
            return await _db.Set<Demande>()
                .Where(d => d.IdUser == userId && d.IsActif)
                .ToListAsync();
        }

        public async Task<List<Demande>> GetByOrganisationIdAsync(int organisationId)
        {
            return await _db.Set<Demande>()
                .Include(d => d.User)
                .Where(d => d.User.OrganisationId == organisationId && d.IsActif)
                .ToListAsync();
        }
    }
}
