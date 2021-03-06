using System.Threading.Tasks;
using smart_dungeons.Domain.Shared;

namespace smart_dungeons.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmartDungeonsDbContext _context;

        public UnitOfWork(SmartDungeonsDbContext context)
        {
            this._context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}