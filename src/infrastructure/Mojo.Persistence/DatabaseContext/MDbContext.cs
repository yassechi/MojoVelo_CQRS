using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mojo.Domain.AI;
using Mojo.Domain.Common;

namespace Mojo.Persistence.DatabaseContext
{
    public class MDbContext : IdentityDbContext<User>
    {
        public MDbContext(DbContextOptions<MDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Configurations externes
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MDbContext).Assembly);

            // 2. User / Organisation
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(u => u.Organisation)
                      .WithMany(o => o.Users)
                      .HasForeignKey(u => u.OrganisationId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(u => u.OrganisationId)
                      .IsUnique(false);
            });

            // 3. Contrats
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

            // 4. Discussions
            modelBuilder.Entity<Discussion>(entity =>
            {
                entity.HasOne(d => d.Client)
                      .WithMany(u => u.Discussions)
                      .HasForeignKey(d => d.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Mojo)
                      .WithMany()
                      .HasForeignKey(d => d.MojoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 4.1 Organisation logos
            modelBuilder.Entity<OrganisationLogo>(entity =>
            {
                entity.HasOne(l => l.Organisation)
                      .WithMany(o => o.OrganisationLogos)
                      .HasForeignKey(l => l.OrganisationId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 5. Types Decimal
            modelBuilder.Entity<Amortissement>().Property(a => a.ValeurInit).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Contrat>().Property(c => c.LoyerMensuelHT).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Intervention>().Property(i => i.Cout).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Velo>().Property(v => v.PrixAchat).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<MoisAmortissement>().Property(m => m.Montant).HasColumnType("decimal(18,2)");

            // 6. Mois d'amortissement
            modelBuilder.Entity<MoisAmortissement>(entity =>
            {
                entity.HasOne(m => m.Amortissement)
                      .WithMany(a => a.MoisAmortissements)
                      .HasForeignKey(m => m.AmortissementId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(m => new { m.AmortissementId, m.NumeroMois })
                      .IsUnique();
            });

            // 7. VuesMessage
            modelBuilder.Entity<VuesMessage>(entity =>
            {
                entity.Property(vm => vm.UserId).IsRequired();

                entity.HasIndex(vm => new { vm.UserId, vm.MessageId })
                      .IsUnique();

                entity.HasOne(vm => vm.User)
                      .WithMany()
                      .HasForeignKey(vm => vm.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(vm => vm.Message)
                      .WithMany()
                      .HasForeignKey(vm => vm.MessageId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity<int>>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedDate = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Entity.ModifiedDate = DateTime.Now;
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        // --- DbSets ---
        public virtual DbSet<Amortissement> Amortissements { get; set; }
        public virtual DbSet<Contrat> Contrats { get; set; }
        public virtual DbSet<Discussion> Discussions { get; set; }
        public virtual DbSet<Intervention> Interventions { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<VuesMessage> VuesMessages { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<Velo> Velos { get; set; }
        public virtual DbSet<Demande> Demandes { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<OrganisationLogo> OrganisationLogos { get; set; }
        public virtual DbSet<MoisAmortissement> MoisAmortissements { get; set; }

        // ✅ DocumentChunk supprimé — géré par InMemory VectorStore
        // ✅ AiLog conservé — logs persistés dans SQL Server
        public DbSet<AiLog> AiLogs { get; set; }
    }
}