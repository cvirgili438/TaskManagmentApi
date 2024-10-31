using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Infrastructure.Models;

namespace TaskApplicationApi.Infrastructure.Persistance
{
    public class UserDbContext : IdentityDbContext<UserEntity>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) :base (options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);
        }
    }
}
