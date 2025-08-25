using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Application.Contracts;
using TaskApplicationApi.Application.Models.Security;
using TaskApplicationApi.Infrastructure.Configuration;
using TaskApplicationApi.Infrastructure.Models;

namespace TaskApplicationApi.Infrastructure.Security
{
    public class IdentityJwtAuthGateway :IAuthGateway
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public IdentityJwtAuthGateway(IUnitOfWork unitOfWork,
                                      UserManager<UserEntity> userManager,
                                      IJwtProvider jwtProvider,
                                      IOptions<JwtOptions> jwtOptions)
        {
            _unitOfWork=unitOfWork;
            _userManager=userManager;
            _jwtProvider=jwtProvider;
            _jwtOptions=jwtOptions;
        }

        public async Task<Result<AuthResponse>> AuthenticateAsync(LogInRequest request, CancellationToken ct = default)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByNameAsync(request.userName);
                if (user is null) return Result.Fail("Credenciales invalidas");
                var ok = await _userManager.CheckPasswordAsync(user, request.password);
                if (!ok) return Result.Fail("Credenciales invalidas");
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtProvider.GenerateToken(user.Id, roles.ToList());
                var refreshToken = _jwtProvider.GenerateRefreshToken();
                //Guardar en futuro repo UserId y RefreshToken
                var refreshTokenExpiredDate = DateTime.UtcNow.AddDays(_jwtOptions.Value.RefreshTokenDays);
                var createRefreshToken =
                    await _unitOfWork.RefreshTokenStore.SaveAsync(user.Id, refreshToken, refreshTokenExpiredDate, ct);
                if (createRefreshToken.IsFailed)
                {
                    return Result.Fail(createRefreshToken.Errors);
                }
                await _unitOfWork.SaveChangeAsync();
                var toReturn = new AuthResponse(token, refreshToken);
                await _unitOfWork.CommitAsync();
                return Result.Ok(toReturn);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<AuthResponse>> RefreshAsync(RefreshTokenRequest request, CancellationToken ct = default)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                //Checking if is valid
                var isValid = await _unitOfWork.RefreshTokenStore.IsValidAsync(request.refreshToken);
                if (isValid.IsFailed) return Result.Fail(isValid.Errors);
                //get userId from token
                var userId = _jwtProvider.DecodeAndGetUserId(request.oldToken);
                //generate new refreshToken 
                var newRefreshToken = _jwtProvider.GenerateRefreshToken();
                var newExpiredAt = DateTime.UtcNow.AddDays(_jwtOptions.Value.RefreshTokenDays);
                //searching user
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    return Result.Fail("Internal Error, User Error.");
                }
                //get roles 
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtProvider.GenerateToken(userId, roles.ToList());
                var resultRotate = await _unitOfWork.RefreshTokenStore.RotateAsync(request.refreshToken, newRefreshToken, newExpiredAt, ct);
                if (resultRotate.IsFailed)
                {
                    return Result.Fail(resultRotate.Errors);
                }
                else
                {
                    var toReturn = new AuthResponse(token, newRefreshToken);
                    await _unitOfWork.SaveChangeAsync();
                    await _unitOfWork.CommitAsync();
                    return Result.Ok(toReturn);
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Fail(ex.Message);
            }
        }
    }
}
