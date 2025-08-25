using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Application.Models.Security;

namespace TaskApplicationApi.Application.Contracts
{
    public interface IAuthGateway
    {
        Task<Result<AuthResponse>> AuthenticateAsync(LogInRequest request, CancellationToken ct = default);
        Task<Result<AuthResponse>> RefreshAsync(RefreshTokenRequest request, CancellationToken ct = default);
    }
}
