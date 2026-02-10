using Mojo.Domain.Entities;

namespace Mojo.Application.Persistance.Contracts
{
    public interface IDocumentRepository : IGenericRepository<Documents>
    {
        Task<List<Documents>> GetByContratIdAsync(int contratId);
    }
}