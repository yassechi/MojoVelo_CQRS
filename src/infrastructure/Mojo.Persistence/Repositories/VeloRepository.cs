using Microsoft.EntityFrameworkCore;
using Mojo.Domain.Entities;

namespace Mojo.Persistence.Repositories
{
    public class VeloRepository : GenericRepository<Velo>, IVeloRepository
    {
        private readonly MDbContext db;

        public VeloRepository(MDbContext db) : base(db)
        {
            this.db = db;
        }

        // ✅ Méthode pour trouver un vélo par numéro de série
        public async Task<Velo?> FindByNumeroSerieAsync(string numeroSerie)
        {
            return await db.Set<Velo>()
                .FirstOrDefaultAsync(v => v.NumeroSerie == numeroSerie);
        }

        // ✅ Méthode pour vérifier si un numéro de série existe déjà
        public async Task<bool> NumeroSerieExists(string numeroSerie, int id)
        {
            var velo = await db.Set<Velo>()
                .FirstOrDefaultAsync(v => v.NumeroSerie == numeroSerie && v.Id != id);

            return velo != null;
        }
    }
}