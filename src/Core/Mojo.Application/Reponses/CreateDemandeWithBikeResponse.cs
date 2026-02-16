namespace Mojo.Application.Reponses
{
    public class CreateDemandeWithBikeResponse : BaseResponse
    {
        public int DemandeId { get; set; }
        public int VeloId { get; set; }
        public int DiscussionId { get; set; }
        public int MessageId { get; set; }
    }
}
