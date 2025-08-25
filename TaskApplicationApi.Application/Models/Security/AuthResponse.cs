using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplicationApi.Application.Models.Security
{
    public sealed record AuthResponse(string token,string refreshToken)
    {
    }
}
