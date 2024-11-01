using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Domain.Entities;

namespace TaskApplicationApi.Infrastructure.Models
{
    public class UserEntity : IdentityUser
    {
        public virtual ICollection<TaskEntity> Tasks {  get; set; }
    }
}
