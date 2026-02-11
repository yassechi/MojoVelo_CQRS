using AutoMapper;
using MediatR;
using Mojo.Application.DTOs.EntitiesDto.Documents;
using Mojo.Application.DTOs.EntitiesDto.Documents.Validators;
using Mojo.Application.Features.Documents.Request.Command;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Documents.Handler.Command
{
    public class CreateDocumentHandler : IRequestHandler<CreateDocumentCommand, BaseResponse>
    {
        private readonly IDocumentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IContratRepository _contratRepository;

        public CreateDocumentHandler(IDocumentRepository repository, IMapper mapper, IContratRepository contratRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _contratRepository = contratRepository;
        }

        public async Task<BaseResponse> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new DocumentValidator(_contratRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la création du document : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }


            var document = _mapper.Map<Mojo.Domain.Entities.Documents>(request.dto);

            //var s = request.dto.File.OpenReadStream();
            //MemoryStream ms = new MemoryStream();
            //s.CopyTo(ms);

            //document.Fichier = ms.ToArray();

            await _repository.CreateAsync(document);

            response.Success = true;
            response.Message = "Le document a été créé avec succès.";
            response.Id = document.Id;
            return response;
        }
    }
}