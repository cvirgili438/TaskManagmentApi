using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplicationApi.Application.Contracts
{
    public interface IJwtProvider
    {
        string GenerateToken(string userId, List<string> roles);
        string GenerateRefreshToken();
        string DecodeAndGetUserId(string token);
    }
}
