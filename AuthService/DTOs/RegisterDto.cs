namespace AuthService.DTOs;

public class RegisterDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Speciality { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}