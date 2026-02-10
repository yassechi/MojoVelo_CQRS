using AutoMapper;
using MediatR;
using Mojo.Application.DTOs.EntitiesDto.Documents;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Documents.Request.Query;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Documents.Handler.Query
{
    public class GetDocumentDetailsHandler : IRequestHandler<GetDocumentDetailsRequest, DocumentDto>
    {
        private readonly IDocumentRepository _repository;
        private readonly IMapper _mapper;

        public GetDocumentDetailsHandler(IDocumentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DocumentDto> Handle(GetDocumentDetailsRequest request, CancellationToken cancellationToken)
        {
            var document = await _repository.GetByIdAsync(request.Id);
            if (document == null || !document.IsActif)
            {
                throw new NotFoundException(nameof(Documents), request.Id);
            }
            return _mapper.Map<DocumentDto>(document);
        }
    }
}