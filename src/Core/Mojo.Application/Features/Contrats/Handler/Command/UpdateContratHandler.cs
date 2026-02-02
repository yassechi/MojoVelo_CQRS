using Mojo.Application.DTOs.EntitiesDto.Contrat.Validators;
using MediatR;
using AutoMapper;
using Mojo.Application.DTOs.Common;

namespace Mojo.Application.Features.Contrats.Handler.Command
{
    public class UpdateContratHandler : IRequestHandler<UpdateContratCommand, BaseResponse>
    {
        private readonly IContratRepository _repository;
        private readonly IMapper _mapper;
        private readonly IVeloRepository _veloRepository;
        private readonly IUserRepository _userRepository;

        public UpdateContratHandler(IContratRepository repository, IMapper mapper, IVeloRepository veloRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _veloRepository = veloRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> Handle(UpdateContratCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new ContratValidator(_veloRepository, _userRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("default", "Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la modification du contrat : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var oldContrat = await _repository.GetByIdAsync(request.dto.Id);
            if (oldContrat == null)
            {
                response.Success = false;
                response.Message = "Echec de la modification du contrat.";
                response.Errors.Add($"Aucun contrat trouvé avec l'Id {request.dto.Id}.");
                return response;
            }

            _mapper.Map(request.dto, oldContrat);
            await _repository.UpdateAsync(oldContrat);

            response.Success = true;
            response.Message = "Le contrat a été modifié avec succès.";
            response.Id = oldContrat.Id;
            return response;
        }
    }
}