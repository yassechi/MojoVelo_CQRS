namespace Mojo.Application.Persistance.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByStringId(string id);

        Task<bool> UserExists(string id);

        Task<bool> UserHasRole(string userId, string roleName);
    }
}