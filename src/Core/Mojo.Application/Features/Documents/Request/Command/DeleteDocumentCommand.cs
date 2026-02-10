using MediatR;

namespace Mojo.Application.Features.Documents.Request.Command
{
    public class DeleteDocumentCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}