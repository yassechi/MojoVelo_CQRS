namespace Mojo.Application.Features.Contrats.Request.Query
{
    public class GetContratDetailsRequest : IRequest<ContratDto>
    {
        public int Id { get; set; }
    }
}