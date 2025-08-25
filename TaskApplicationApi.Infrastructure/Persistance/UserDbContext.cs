using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Domain.Entities;
using TaskApplicationApi.Infrastructure.Models;
using TaskApplicationApi.Infrastructure.Security.Model;

namespace TaskApplicationApi.Infrastructure.Persistance
{
    public class UserDbContext : IdentityDbContext<UserEntity>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) :base (options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<TaskEntity>().OwnsOne(e=>e.Description,config =>
            {
                config.Property(e => e.Value).HasColumnName("Description").HasMaxLength(255).IsRequired(true);
            });
            builder.Entity<TaskEntity>().OwnsOne(e=>e.Status,config =>
            {
                config.Property(e => e.Value).HasColumnName("Status").IsRequired(true);
            });
            builder.Entity<TaskEntity>().OwnsOne(e => e.CreationDate, config =>
            {
                config.Property(e => e.Value).HasColumnName("CreationDate");
            });
            builder.Entity<TaskEntity>().OwnsOne(e=>e.DueDate,config =>
            {
                config.Property(e => e.Value).HasColumnName("DueDate");
            });
            builder.Entity<TaskEntity>().OwnsOne(e=>e.Priority,config =>
            {
                config.Property(e => e.Value).HasColumnName("Priority");
            });
            builder.Entity<TaskEntity>().HasOne<UserEntity>().WithMany(e=>e.Tasks).HasForeignKey(e=>e.UserId);
            builder.Entity<TaskEntity>().HasMany(e => e.Tags).WithMany(e=>e.Tasks);
            builder.Entity<RefreshTokenModel>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(x => x.TokenHash).IsUnique();
                e.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.UserId);
            });
            base.OnModelCreating(builder);
        }
        public DbSet<TaskEntity> Task { get; set; }
        public DbSet<TaskTags> TaskTags { get; set; }
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }
    }
}
