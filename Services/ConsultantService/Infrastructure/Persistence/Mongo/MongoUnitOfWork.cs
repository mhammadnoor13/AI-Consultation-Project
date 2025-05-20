using Application.Common.Persistence;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Mongo
{
    internal sealed class MongoUnitOfWork : IUnitOfWork
    {
        private readonly IClientSessionHandle _sessionHandle;

        public MongoUnitOfWork() { }
        public Task CommitAsync(CancellationToken ct = default) => Task.CompletedTask;

        public Task RollbackAsync(CancellationToken ct = default) =>Task.CompletedTask;
    }
}
