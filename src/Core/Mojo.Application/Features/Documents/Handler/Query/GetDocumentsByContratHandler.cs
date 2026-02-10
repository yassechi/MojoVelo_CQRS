using AutoMapper;
using MediatR;
using Mojo.Application.DTOs.EntitiesDto.Documents;
using Mojo.Application.Features.Documents.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Documents.Handler.Query
{
    public class GetDocumentsByContratHandler : IRequestHandler<GetDocumentsByContratRequest, List<DocumentDto>>
    {
        private readonly IDocumentRepository _repository;
        private readonly IMapper _mapper;

        public GetDocumentsByContratHandler(IDocumentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DocumentDto>> Handle(GetDocumentsByContratRequest request, CancellationToken cancellationToken)
        {
            var documents = await _repository.GetByContratIdAsync(request.ContratId);
            return _mapper.Map<List<DocumentDto>>(documents);
        }
    }
}