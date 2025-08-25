using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplicationApi.Application.Contracts
{
    public interface IUnitOfWork
    {
        IRefreshTokenStore RefreshTokenStore { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task<int> SaveChangeAsync();
    }
}
