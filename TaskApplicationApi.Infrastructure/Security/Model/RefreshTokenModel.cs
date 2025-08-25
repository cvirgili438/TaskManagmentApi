using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplicationApi.Infrastructure.Security.Model
{
    public class RefreshTokenModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }                 // FK a AspNetUsers(Id)
        public string TokenHash { get; set; } = null!;   // SHA-256 del token
        public DateTime CreatedAtUtc { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime? RevokedAtUtc { get; set; }
        public string? ReplacedByHash { get; set; }
    }
}
