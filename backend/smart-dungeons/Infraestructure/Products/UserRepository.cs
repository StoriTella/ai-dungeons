using smart_dungeons.Domain.Users;
using smart_dungeons.Infrastructure.Shared;

namespace smart_dungeons.Infrastructure.Users
{
    public class UserRepository : BaseRepository<User, UserId>,IUserRepository
    {
        public UserRepository(SmartDungeonsDbContext context):base(context.Users)
        {
           
        }
    }
}