using System.ComponentModel.DataAnnotations.Schema;

namespace Mojo.Domain.Entities
{
    public class Organisation : BaseEntity<Organisation>
    {

        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public bool IsActif { get; set; }

        [ForeignKey(nameof(User))]
        public string IdContact { get; set; }
        public User Contact { get; set; }

        public string? LogoUrl { get; set; }
        public string EmailAutorise { get; set; } = string.Empty;


        public List<User> Users { get; set; } = [];
    }
}