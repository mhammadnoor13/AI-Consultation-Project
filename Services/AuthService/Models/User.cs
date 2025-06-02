using MongoDB.Bson.Serialization.Attributes;

namespace AuthService.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public User() { }

    public User Create(string email, string passwordhash)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = passwordhash,
        };
        }
    }


