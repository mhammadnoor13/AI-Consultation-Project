using AuthService.Interfaces;
using AuthService.Models;
using MongoDB.Driver;

namespace AuthService.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(IMongoDatabase db)
    {
        _collection = db.GetCollection<User>("Users");
    }

    public Task<User?> GetByEmailAsync(string email)
        => _collection.Find(u => u.Email == email).FirstOrDefaultAsync();

    public Task<bool> ExistsAsync(string email)
        => _collection.Find(u => u.Email == email).AnyAsync();

    public Task AddAsync(User user)
        => _collection.InsertOneAsync(user);
}
