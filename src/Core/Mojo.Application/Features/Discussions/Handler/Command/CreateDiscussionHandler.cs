using Mojo.Application.DTOs.EntitiesDto.Discussion.Validators;

namespace Mojo.Application.Features.Discussion.Handler.Command
{
    public class CreateDiscussionHandler : IRequestHandler<CreateDiscussionCommand, BaseResponse>
    {
        private readonly IDiscussionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateDiscussionHandler(IDiscussionRepository repository, IMapper mapper, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> Handle(CreateDiscussionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var validator = new DiscussionValidator(_userRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la création de la discussion : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var discussion = _mapper.Map<Mojo.Domain.Entities.Discussion>(request.dto);
            await _repository.CreateAsync(discussion);

            response.Success = true;
            response.Message = "La discussion a été créée avec succès.";
            response.Id = discussion.Id;
            return response;
        }
    }
}