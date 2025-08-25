using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplicationApi.Infrastructure.Configuration
{
    public class JwtOptions
    {
        public const string SectionName = "JwtSettings";
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public string Key { get; init; }           // Symmetric key (mínimo 256 bits)
        public int AccessTokenMinutes { get; init; } = 30;
        public int RefreshTokenDays { get; init; } = 7;
    }
}
