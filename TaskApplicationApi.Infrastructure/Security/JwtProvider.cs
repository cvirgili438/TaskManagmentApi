using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Application.Contracts;
using TaskApplicationApi.Infrastructure.Configuration;

namespace TaskApplicationApi.Infrastructure.Security
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IOptions<JwtOptions> _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options=options;
        }

        public string GenerateToken(string userId, List<string>? roles)
        {
            try
            {
                if (string.IsNullOrEmpty(_options.Value.Key))
                {
                    throw new FileLoadException("Key in AppSetting not found");
                }
                if (_options.Value.AccessTokenMinutes == 0)
                {
                    throw new FileLoadException("AccessTokenMinute in AppSetting not found");
                }
                var keyBytes = Encoding.ASCII.GetBytes(_options.Value.Key);
                var creds = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>();
                if (roles is not null  && roles.Any())
                {
                    foreach (var rol in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, rol));
                    }
                }
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userId));
                var token = new JwtSecurityToken(
                        issuer: _options.Value.Issuer,      // <- importante
                        audience: _options.Value.Audience,    // <- importante
                        claims: claims,
                        notBefore: DateTime.UtcNow,
                        expires: DateTime.UtcNow.AddMinutes(_options.Value.AccessTokenMinutes),
                        signingCredentials: creds);
                var tokenHandler = new JwtSecurityTokenHandler();
                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateRefreshToken()
        {
            var byteArray = new byte[64];
            var refreshToken = string.Empty;
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }
            return refreshToken;
        }

        public string DecodeAndGetUserId(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    throw new ArgumentException("Token vacío.", nameof(token));
                if (string.IsNullOrEmpty(_options.Value.Key))
                {
                    throw new FileLoadException("Key in AppSetting not found");
                }
                var handler = new JwtSecurityTokenHandler();
                var tvp = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Value.Key)),

                    ValidateIssuer = true,
                    ValidIssuer = _options.Value.Issuer,

                    ValidateAudience = true,
                    ValidAudience = _options.Value.Audience,

                    // Si querés poder leer un token vencido para extraer el id, poné false:
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.FromSeconds(30)    
                };
                var principal = handler.ValidateToken(token, tvp, out _);
                var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                if (string.IsNullOrWhiteSpace(id))
                    throw new SecurityTokenException("El token no contiene un identificador de usuario.");

                return id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
