using smart_dungeons.Domain.Shared;

namespace smart_dungeons.Domain.Users
{
    public interface IUserRepository: IRepository<User,UserId>
    {    
    }
}