using MediatR;
using Mojo.Application.DTOs.EntitiesDto.Documents;

namespace Mojo.Application.Features.Documents.Request.Query
{
    public class GetAllDocumentRequest : IRequest<List<DocumentDto>>
    {
    }
}