using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Application.Models.Security;

namespace TaskApplicationApi.Application.Interfases
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> LogIn(LogInRequest request);
        Task<Result<AuthResponse>> RefreshToken(RefreshTokenRequest request);
    }
}
