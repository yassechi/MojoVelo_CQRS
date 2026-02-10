using MediatR;
using Mojo.Application.DTOs.EntitiesDto.Documents;

namespace Mojo.Application.Features.Documents.Request.Command
{
    public class UpdateDocumentCommand : IRequest<BaseResponse>
    {
        public DocumentDto dto { get; set; }
    }
}