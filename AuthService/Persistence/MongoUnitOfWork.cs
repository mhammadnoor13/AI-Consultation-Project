using AuthService.Interfaces;

namespace AuthService.Persistence;

public class MongoUnitOfWork : IUnitOfWork
{
    public Task CommitAsync() => Task.CompletedTask; // Mongo auto-commits
}
