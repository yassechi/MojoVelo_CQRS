using Mojo.Application.Features.Velos.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Velos.Handler.Query
{
    public class GetVeloTypesHandler : IRequestHandler<GetVeloTypesRequest, List<string>>
    {
        private readonly IVeloRepository _repository;

        public GetVeloTypesHandler(IVeloRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<string>> Handle(GetVeloTypesRequest request, CancellationToken cancellationToken)
        {
            var velos = await _repository.GetAllAsync();

            return velos
                .Where(v => v.IsActif && !string.IsNullOrWhiteSpace(v.Type))
                .Select(v => v.Type!.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(type => type, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
    }
}
