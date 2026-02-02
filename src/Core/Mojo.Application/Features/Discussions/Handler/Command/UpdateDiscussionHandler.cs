using Mojo.Application.DTOs.EntitiesDto.Discussion.Validators;

namespace Mojo.Application.Features.Discussion.Handler.Command
{
    public class UpdateDiscussionHandler : IRequestHandler<UpdateDiscussionCommand, BaseResponse>
    {
        private readonly IDiscussionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UpdateDiscussionHandler(IDiscussionRepository repository, IMapper mapper, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> Handle(UpdateDiscussionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new DiscussionValidator(_userRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la modification de la discussion : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var oldDiscussion = await _repository.GetByIdAsync(request.dto.Id);
            if (oldDiscussion == null)
            {
                response.Success = false;
                response.Message = "Echec de la modification de la discussion.";
                response.Errors.Add($"Aucune discussion trouvée avec l'Id {request.dto.Id}.");
                return response;
            }

            _mapper.Map(request.dto, oldDiscussion);
            await _repository.UpdateAsync(oldDiscussion);

            response.Success = true;
            response.Message = "La discussion a été modifiée avec succès.";
            response.Id = oldDiscussion.Id;
            return response;
        }
    }
}