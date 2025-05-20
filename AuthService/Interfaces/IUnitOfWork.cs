namespace AuthService.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync(); // no-op in Mongo but keeps pattern intact
}
