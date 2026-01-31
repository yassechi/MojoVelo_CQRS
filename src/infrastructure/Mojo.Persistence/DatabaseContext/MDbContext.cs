using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mojo.Domain.Common;

namespace Mojo.Persistence.DatabaseContext
{
    public class MDbContext : IdentityDbContext<User>
    {
        public MDbContext(DbContextOptions<MDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MDbContext).Assembly);

            modelBuilder.Entity<Contrat>()
                .HasOne(c => c.Beneficiaire)
                .WithMany(u => u.ContratsRecus)
                .HasForeignKey(c => c.BeneficiaireId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contrat>()
                .HasOne(c => c.UserRH)
                .WithMany(u => u.ContratsGeres)
                .HasForeignKey(c => c.UserRhId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Discussion>(entity =>
            {
                // Configuration pour le Client
                entity.HasOne(d => d.Client)
                      .WithMany(u => u.Discussions)
                      .HasForeignKey(d => d.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Configuration pour le Mojo (Support/Admin)
                entity.HasOne(d => d.Mojo)
                      .WithMany() 
                      .HasForeignKey(d => d.MojoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Dans OnModelCreating
            modelBuilder.Entity<Amortissement>().Property(a => a.ValeurInit).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Contrat>().Property(c => c.LoyerMensuelHT).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Intervention>().Property(i => i.Cout).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Velo>().Property(v => v.PrixAchat).HasColumnType("decimal(18,2)");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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