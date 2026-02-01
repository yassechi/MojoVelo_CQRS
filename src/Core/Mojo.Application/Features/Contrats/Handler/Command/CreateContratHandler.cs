using Mojo.Application.DTOs.EntitiesDto.Contrat.Validators;
using MediatR;
using AutoMapper;
using Mojo.Application.DTOs.Common;

namespace Mojo.Application.Features.Contrats.Handler.Command
{
    public class CreateContratHandler : IRequestHandler<CreateContratCommand, BaseResponse>
    {
        private readonly IContratRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;
        private readonly IUserRepository _userRepository;

        public CreateContratHandler(IContratRepository repository, IMapper mapper, IVeloRepository veloRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> Handle(CreateContratCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new ContratValidator(_veloRepository, _userRepository);

            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("default", "Create");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de la création du contrat : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var contrat = _mapper.Map<Mojo.Domain.Entities.Contrat>(request.dto);
            await _repository.CreateAsync(contrat);

            response.Succes = true;
            response.Message = "Le contrat a été créé avec succès.";
            response.Id = contrat.Id;

            return response;
        }
    }
}