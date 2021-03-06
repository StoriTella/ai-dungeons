using smart_dungeons.Domain.Users;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using smart_dungeons.Infrastructure.Shared;
namespace smart_dungeons.Infrastructure.Users
{
    public class UserRepository : BaseRepository<User,UserId>, IUserRepository
    {
        private readonly SmartDungeonsDbContext _context;
        public UserRepository(SmartDungeonsDbContext context):base(context.Users)
        {
            this._context = context;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.Where(u => u.Username == username).SingleOrDefaultAsync();
        }
    }
}