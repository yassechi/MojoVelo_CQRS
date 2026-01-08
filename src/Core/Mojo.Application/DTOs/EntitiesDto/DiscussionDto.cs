
namespace Mojo.Application.DTOs.EntitiesDto
{
    //public class DiscussionAddDto : BaseEntity<int>
    public class DiscussionDto : BaseEntity<int>
    {
        public string Objet { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime DateCreation { get; set; }
        public string ClientId { get; set; } = null!;
        public string MojoId { get; set; } = null!;
    }

    //public class DiscussionUpdateDto : BaseEntity<int>
    //{
    //    public string Objet { get; set; } = null!;
    //    public bool Status { get; set; }
    //    public DateTime DateCreation { get; set; }
    //    public string ClientId { get; set; } = null!;
    //    public string MojoId { get; set; } = null!;
    //}
}