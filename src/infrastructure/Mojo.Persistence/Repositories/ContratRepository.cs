using Microsoft.EntityFrameworkCore;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;
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
        private readonly MDbContext _db;

        public ContratRepository(MDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Contrat>> GetByUserIdAsync(string userId)
        {
            return await _db.Set<Contrat>()
                .Where(c => c.BeneficiaireId == userId && c.IsActif)
                .ToListAsync();
        }

        public async Task<List<Contrat>> GetByOrganisationIdAsync(int organisationId)
        {
            return await _db.Set<Contrat>()
                .Include(c => c.Beneficiaire)
                .Where(c => c.Beneficiaire.OrganisationId == organisationId && c.IsActif)
                .ToListAsync();
        }
    }
}
