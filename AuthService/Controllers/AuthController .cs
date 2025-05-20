using AuthService.DTOs;
using AuthService.Interfaces;
using AuthService.Models;
using AuthService.Utils;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Contracts;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _users;
    private readonly IUnitOfWork _uow;
    private readonly IConfiguration _config;
    private readonly IPublishEndpoint _publishEndpoint;


    public AuthController(IUserRepository users, IUnitOfWork uow, IConfiguration config,IPublishEndpoint publishEndpoint)
    {
        _users = users;
        _uow = uow;
        _config = config;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (await _users.ExistsAsync(dto.Email))
            return BadRequest("Email already exists.");

        var user = new User
        {
            Email = dto.Email,
            PasswordHash = PasswordHasher.Hash(dto.Password)
        };

        await _users.AddAsync(user);
        await _publishEndpoint.Publish<IUserRegistered>(new
        {
            Id = user.Id,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Speciality = dto.Speciality
        });
        await _uow.CommitAsync();


        var token = JwtTokenGenerator.Generate(dto.Email, _config);
        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _users.GetByEmailAsync(dto.Email);
        if (user is null || !PasswordHasher.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials.");

        var token = JwtTokenGenerator.Generate(user.Email, _config);
        return Ok(new { token });
    }
}
