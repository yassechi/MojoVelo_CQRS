using AutoMapper;
using MediatR;
using Mojo.Application.DTOs.EntitiesDto.Documents.Validators;
using Mojo.Application.Features.Documents.Request.Command;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Documents.Handler.Command
{
    public class UpdateDocumentHandler : IRequestHandler<UpdateDocumentCommand, BaseResponse>
    {
        private readonly IDocumentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IContratRepository _contratRepository;

        public UpdateDocumentHandler(IDocumentRepository repository, IMapper mapper, IContratRepository contratRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _contratRepository = contratRepository;
        }

        public async Task<BaseResponse> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new DocumentValidator(_contratRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la modification du document : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var oldDocument = await _repository.GetByIdAsync(request.dto.Id);
            if (oldDocument == null)
            {
                response.Success = false;
                response.Message = "Echec de la modification du document.";
                response.Errors.Add($"Aucun document trouvé avec l'Id {request.dto.Id}.");
                return response;
            }

            _mapper.Map(request.dto, oldDocument);
            await _repository.UpdateAsync(oldDocument);

            response.Success = true;
            response.Message = "Le document a été modifié avec succès.";
            response.Id = oldDocument.Id;
            return response;
        }
    }
}