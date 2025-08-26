using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Application.Contracts;
using TaskApplicationApi.Application.Interfases;
using TaskApplicationApi.Application.Models.Security;

namespace TaskApplicationApi.Application.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IAuthGateway _authGateway;

        public AuthService(IAuthGateway authGateway)
        {
            _authGateway=authGateway;
        }
        public async Task<Result<AuthResponse>> LogIn(LogInRequest request)
        {
            var response = await _authGateway.AuthenticateAsync(request);
            return response;
        }

        public async Task<Result<AuthResponse>> RefreshToken(RefreshTokenRequest request)
        {
            var response = await _authGateway.RefreshAsync(request);
            return response;
        }
    }
}
