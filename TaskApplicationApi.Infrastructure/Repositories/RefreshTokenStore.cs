using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Application.Contracts;
using TaskApplicationApi.Infrastructure.Persistance;
using TaskApplicationApi.Infrastructure.Security.Model;

namespace TaskApplicationApi.Infrastructure.Repositories
{
    public class RefreshTokenStore : IRefreshTokenStore
    {
        private readonly UserDbContext _db;

        public RefreshTokenStore(UserDbContext db)
        {
            _db=db;
        }
        static string Hash(string plain)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(plain)));
        }
        public async Task<Result> SaveAsync(string userId, string refreshPlain, DateTime expiresUtc, CancellationToken ct = default)
        {
            await _db.RefreshTokens.AddAsync(new RefreshTokenModel
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TokenHash = Hash(refreshPlain),
                CreatedAtUtc = DateTime.UtcNow,
                ExpiresAtUtc = expiresUtc
            });
            return Result.Ok();
        }


        public async Task<Result> IsValidAsync(string refreshPlain, CancellationToken ct = default)
        {
            var h = Hash(refreshPlain);
            var now = DateTime.UtcNow;
            var resul = await _db.RefreshTokens
                .AnyAsync(x => x.TokenHash == h && x.RevokedAtUtc == null && now < x.ExpiresAtUtc, ct);
            return resul is true ? Result.Ok() : Result.Fail("Is not valid");
        }
        public async Task<Result> RotateAsync(string oldPlain, string newPlain, DateTime newExp, CancellationToken ct = default)
        {
            var oldHash = Hash(oldPlain);
            var ent = await _db.RefreshTokens.SingleOrDefaultAsync(x => x.TokenHash == oldHash, ct);
            if (ent is null) return Result.Fail("Token not found");
            ent.RevokedAtUtc = DateTime.UtcNow;
            ent.ReplacedByHash = Hash(newPlain);

            await _db.RefreshTokens.AddAsync(new RefreshTokenModel
            {
                Id = Guid.NewGuid(),
                UserId = ent.UserId,
                TokenHash = ent.ReplacedByHash,
                CreatedAtUtc = DateTime.UtcNow,
                ExpiresAtUtc = newExp
            }, ct);
            return Result.Ok();
        }
        public async Task<Result> RevokeChainAsync(string userId, string reason, CancellationToken ct = default)
        {
            try
            {
                var now = DateTime.UtcNow;
                await _db.RefreshTokens
                    .Where(x => x.UserId == userId && x.RevokedAtUtc == null)
                    .ExecuteUpdateAsync(s => s.SetProperty(x => x.RevokedAtUtc, now), ct);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> RevokeAllSesions(string userId, CancellationToken ct = default)
        {
            try
            {
                await _db.RefreshTokens.Where(e => e.UserId == userId).ExecuteDeleteAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> CheckIfAlreadyUse(string userId, string refreshPlain)
        {
            try
            {
                var hashRefresh = Hash(refreshPlain);
                var searchByTokenHash = await _db.RefreshTokens.Where(e=>e.UserId == userId && e.TokenHash == hashRefresh ).SingleOrDefaultAsync();
                bool checkIfRevoked = searchByTokenHash.RevokedAtUtc.HasValue 
                    && searchByTokenHash.RevokedAtUtc.Value <= DateTime.UtcNow;
                if (!string.IsNullOrEmpty(searchByTokenHash.ReplacedByHash) 
                    || checkIfRevoked) 
                {
                    return Result.Fail("Already used");
                }
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
