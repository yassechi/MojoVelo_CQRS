using AutoMapper;
using MediatR;
using Mojo.Application.DTOs.EntitiesDto.Documents;
using Mojo.Application.Features.Documents.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Documents.Handler.Query
{
    public class GetAllDocumentHandler : IRequestHandler<GetAllDocumentRequest, List<DocumentDto>>
    {
        private readonly IDocumentRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDocumentHandler(IDocumentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DocumentDto>> Handle(GetAllDocumentRequest request, CancellationToken cancellationToken)
        {
            var documents = await _repository.GetAllAsync();
            var documentsActifs = documents.Where(d => d.IsActif).ToList();
            return _mapper.Map<List<DocumentDto>>(documentsActifs);
        }
    }
}