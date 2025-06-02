using AuthService.Models;

namespace AuthService.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> ExistsAsync(string email);
    Task AddAsync(User user);
}