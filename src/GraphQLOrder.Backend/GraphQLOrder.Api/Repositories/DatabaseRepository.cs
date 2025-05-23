﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace GraphQLOrder.Api.Repositories
{
    public class DatabaseRepository<TContext> : IDatabaseRepository<TContext> where TContext : DbContext
    {
        private readonly IDbContextFactory<TContext> dbContextFactory;

        public DatabaseRepository(IDbContextFactory<TContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        #region IDatabaseRepository Members

        public async Task MigrateDatabaseAsync(CancellationToken cancellationToken)
        {
            using var dbContext = await CreateDbContextAsync(cancellationToken);
            await dbContext.Database.MigrateAsync(cancellationToken);
        }

        public async Task<TContext> GetDbContextAsync(CancellationToken cancellationToken)
        {
            return await CreateDbContextAsync(cancellationToken);
        }

        public IQueryable<T> Query<T>(TContext dbContext) where T : class
        {
            return dbContext.Set<T>().AsQueryable();
        }

        public async Task<T> AddAsync<T>(TContext dbContext, T obj, CancellationToken cancellationToken) where T : class
        {
            var addedEntity = await dbContext.AddAsync(obj, cancellationToken);
            return addedEntity.Entity;
        }

        public T Update<T>(TContext dbContext, T obj) where T : class
        {
            return dbContext.Update(obj).Entity;
        }

        public void Remove<T>(TContext dbContext, T obj) where T : class
        {
            dbContext.Remove(obj);
        }

        public async Task SaveChangesAsync(TContext dbContext, CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(TContext dbContext, IsolationLevel isolationLevel, CancellationToken cancellationToken)
        {
            return await dbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        #endregion

        protected async Task<TContext> CreateDbContextAsync(CancellationToken cancellationToken)
        {
            return await dbContextFactory.CreateDbContextAsync(cancellationToken);
        }
    }
}
