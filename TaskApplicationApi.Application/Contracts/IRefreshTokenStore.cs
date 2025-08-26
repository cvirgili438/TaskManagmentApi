using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplicationApi.Application.Contracts
{
    public interface IRefreshTokenStore
    {
        Task<Result> SaveAsync(string userId, string refreshPlain, DateTime expiresUtc, CancellationToken ct = default);
        Task<Result> IsValidAsync(string refreshPlain, CancellationToken ct = default);
        Task<Result> RotateAsync(string oldPlain, string newPlain, DateTime newExp, CancellationToken ct = default);
        Task<Result> RevokeChainAsync(string userId, string reason, CancellationToken ct = default);
        Task<Result> RevokeAllSesions(string userId, CancellationToken ct = default);
        Task<Result> CheckIfAlreadyUse(string userId, string refreshPlain);
    }
}
