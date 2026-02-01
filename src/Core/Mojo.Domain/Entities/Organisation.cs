namespace Mojo.Domain.Entities
{
    public class Organisation : BaseEntity<Organisation>
    {

        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public bool IsActif { get; set; }

        List<User> Users { get; set; } = [];
    }
}