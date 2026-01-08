namespace Mojo.Application.DTOs.EntitiesDto.Message
{
    //public class MessageAddDto : BaseEntity<int>
    public class MessageDto : BaseEntity<int>
    {
        public string Contenu { get; set; } = null!;
        public DateTime DateEnvoi { get; set; }
        public string UserId { get; set; } = null!;
        public int DiscussionId { get; set; }
    }

    //public class MessageUpdateDto : BaseEntity<int>
    //{
    //    public string Contenu { get; set; } = null!;
    //    public DateTime DateEnvoi { get; set; }
    //    public string UserId { get; set; } = null!;
    //    public int DiscussionId { get; set; }

    //}
}