using AutoMapper;
using MediatR;
using Mojo.Application.Features.Documents.Request.Command;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Documents.Handler.Command
{
    public class DeleteDocumentHandler : IRequestHandler<DeleteDocumentCommand, BaseResponse>
    {
        private readonly IDocumentRepository _repository;
        private readonly IMapper _mapper;

        public DeleteDocumentHandler(IDocumentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var document = await _repository.GetByIdAsync(request.Id);

            if (document == null)
            {
                response.Success = false;
                response.Message = "Echec de la désactivation du document.";
                response.Errors.Add($"Aucun document trouvé avec l'Id {request.Id}.");
                return response;
            }

            // Au lieu de supprimer, on désactive le document
            document.IsActif = false;
            await _repository.UpdateAsync(document);

            response.Success = true;
            response.Message = "Le document a été désactivé avec succès.";
            response.Id = request.Id;
            return response;
        }
    }
}