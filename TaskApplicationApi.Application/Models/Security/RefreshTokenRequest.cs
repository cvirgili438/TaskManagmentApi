using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplicationApi.Application.Models.Security
{
    public class RefreshTokenRequest
    {
        public string oldToken { get; set; }
        public string refreshToken { get; set; }
    }
}
