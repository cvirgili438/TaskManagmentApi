using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Application.Contracts;

namespace TaskApplicationApi.Infrastructure.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _context;
        private readonly IRefreshTokenStore _refreshTokenStore;
        private IDbContextTransaction? _transaction;


        public UnitOfWork(UserDbContext context, IRefreshTokenStore refreshTokenStore)
        {
            _context=context;
            _refreshTokenStore=refreshTokenStore;
        }
        public IRefreshTokenStore RefreshTokenStore => _refreshTokenStore;
        public async Task BeginTransactionAsync()
        {
            _transaction= await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }

        public async Task<int> SaveChangeAsync() => await _context.SaveChangesAsync();
       
    }
}
