using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplicationApi.Application.Models.Security
{
    public class LogInRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
