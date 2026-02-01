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

            var res = await validator.ValidateAsync(request.dto, cancellationToken);

            if (!res.IsValid)
            {
                response.Succes = false;
                response.Message = "Echec de creation de la discussion !";
                response.Errors = res.Errors.Select(e => e.ErrorMessage).ToList();
            }

            response.Succes = true;
            response.Message = "Creation de la discussion avec succès..";
            response.Id = request.dto.Id;

            var discussion = _mapper.Map<Mojo.Domain.Entities.Discussion>(request.dto);

            await _repository.CreateAsync(discussion);

            return response;
        }
    }
}