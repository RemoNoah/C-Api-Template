using DotnetApiTemplate.Api.Auth;
using DotnetApiTemplate.Domain.DTO;
using DotnetApiTemplate.Domain.Models;
using DotnetApiTemplate.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApiTemplate.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly JWTokenGenerator _jwtGenerator;
    private readonly IUserService _userService;
    private readonly int _expirationMinutes;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _key;

    public AuthController(IConfiguration configuration, IUserService userService)
    {
        _config = configuration;
        _userService = userService;
        _key = _config.GetValue<string>("Jwt:Key")!;
        _expirationMinutes = _config.GetValue<int>("Jwt:ExpireMinutes");
        _issuer = _config.GetValue<string>("Jwt:Issuer")!;
        _audience = _config.GetValue<string>("Jwt:Audience")!;

        _jwtGenerator = new JWTokenGenerator();
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult> Register(UserRegistrationDTO user)
    {
        if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
        {
            return BadRequest("Enter all User infos");
        }
        User? registeredUser = await _userService.RegisterAsync(user);

        if (registeredUser != null)
        {
            return Ok(_jwtGenerator.GenerateToken(registeredUser, _key, _expirationMinutes, _issuer, _audience));
        }
        return BadRequest("Email already Exists");
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult> Login(UserLoginDTO user)
    {
        if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
        {
            return BadRequest("Enter all User infos");
        }

        User? logedInUser = await _userService.LoginAsync(user);

        if (logedInUser != null)
        {
            return Ok(_jwtGenerator.GenerateToken(logedInUser, _key, _expirationMinutes, _issuer, _audience));
        }
        return BadRequest("Username or Passwort are wrong");
    }
}
