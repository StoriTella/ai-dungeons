using smart_dungeons.Domain.Shared;
using System.Threading.Tasks;
namespace smart_dungeons.Domain.Users
{
    public interface IUserRepository: IRepository<User,UserId>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}