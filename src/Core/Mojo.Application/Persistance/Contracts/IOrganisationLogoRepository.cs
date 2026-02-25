using Mojo.Domain.Entities;

namespace Mojo.Application.Persistance.Contracts
{
    public interface IOrganisationLogoRepository : IGenericRepository<OrganisationLogo>
    {
        Task<List<OrganisationLogo>> GetByOrganisationIdAsync(int organisationId);
        Task DeactivateOtherLogosAsync(int organisationId, int? keepLogoId = null);
    }
}
