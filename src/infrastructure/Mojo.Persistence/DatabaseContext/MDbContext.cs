using Microsoft.EntityFrameworkCore;
using Mojo.Domain.Common;
using Mojo.Domain.Entities;


namespace Mojo.Persistence.DatabaseContext
{
    public class MDbContext:Microsoft.EntityFrameworkCore.DbContext
    {
        public MDbContext(DbContextOptions options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MDbContext).Assembly);
        }

        // Manage Created && updated At 
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken  = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity<int>>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedDate = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public virtual DbSet<Amortissement> Amortissements { get; set; }
        public virtual DbSet<Contrat> Contrats { get; set; }
        public virtual DbSet<Discussion> Discussions { get; set; }
        public virtual DbSet<Intervention> Interventions { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<Velo> Velos { get; set; }
    }
}
