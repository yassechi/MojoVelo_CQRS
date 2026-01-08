namespace Mojo.Application.Persistance.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByStringId(string id);
    }
}