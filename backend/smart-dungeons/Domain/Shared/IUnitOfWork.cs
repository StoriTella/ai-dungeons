using System.Threading.Tasks;

namespace smart_dungeons.Domain.Shared
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}