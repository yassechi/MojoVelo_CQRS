using MediatR;

namespace Mojo.Application.Persistance.Contracts
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        public Task<List<Message>> GetByDiscussionId(int id);
    }
}