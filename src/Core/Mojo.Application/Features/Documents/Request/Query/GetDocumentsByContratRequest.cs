using MediatR;
using Mojo.Application.DTOs.EntitiesDto.Documents;

namespace Mojo.Application.Features.Documents.Request.Query
{
    public class GetDocumentsByContratRequest : IRequest<List<DocumentDto>>
    {
        public int ContratId { get; set; }
    }
}