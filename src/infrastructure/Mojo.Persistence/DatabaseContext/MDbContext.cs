using Microsoft.EntityFrameworkCore;
using Mojo.Domain.Entities;


namespace Mojo.Persistence.DatabaseContext
{
    internal class MDbContext:Microsoft.EntityFrameworkCore.DbContext
    {
        public MDbContext(DbContextOptions options) : base(options){}

        public virtual DbSet<Amortissement> Amortissements { get; set; }
        public virtual DbSet<Contrat> Contrats { get; set; }
        public virtual DbSet<Discussion> Discussions { get; set; }
        public virtual DbSet<Intervention> Interventions { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<Velo> Velos { get; set; }
    }
}
