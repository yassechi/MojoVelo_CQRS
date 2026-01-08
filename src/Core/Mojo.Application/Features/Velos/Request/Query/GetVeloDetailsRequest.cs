namespace Mojo.Application.Features.Velos.Request.Query
{
    internal class GetVeloDetailsRequest : IRequest<VeloDto>
    {
        public int Id { get; set; }
    }
}
