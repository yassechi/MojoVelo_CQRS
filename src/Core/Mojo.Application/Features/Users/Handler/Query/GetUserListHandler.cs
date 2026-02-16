using Mojo.Application.DTOs.EntitiesDto.User;
using Mojo.Application.Features.Users.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Users.Handler.Query
{
    public class GetUserListHandler : IRequestHandler<GetUserListRequest, List<AdminUserListItemDto>>
    {
        private readonly IUserRepository _repository;

        public GetUserListHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AdminUserListItemDto>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();

            var search = Normalize(request.Search);

            var filtered = users.Where(user =>
            {
                if (request.Role.HasValue && user.Role != request.Role.Value)
                {
                    return false;
                }

                if (request.IsActif.HasValue && user.IsActif != request.IsActif.Value)
                {
                    return false;
                }

                if (request.OrganisationId.HasValue && user.OrganisationId != request.OrganisationId.Value)
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(search))
                {
                    return true;
                }

                return ContainsNormalized(user.UserName, search) ||
                       ContainsNormalized(user.FirstName, search) ||
                       ContainsNormalized(user.LastName, search) ||
                       ContainsNormalized(user.Email, search) ||
                       ContainsNormalized(user.PhoneNumber, search);
            });

            return filtered.Select(user => new AdminUserListItemDto
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Role = user.Role,
                IsActif = user.IsActif,
                OrganisationId = user.OrganisationId
            }).ToList();
        }

        private static string Normalize(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToLowerInvariant();
        }

        private static bool ContainsNormalized(string? source, string search)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(search))
            {
                return false;
            }
            return source.Trim().ToLowerInvariant().Contains(search);
        }
    }
}
