using DotnetApiTemplate.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotnetApiTemplate.Api.Auth;

public class JwtTokenGenerator
{
    public string GenerateToken(User user, string secretKey, int expirationMinutes)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        foreach (Role role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role?.Name ?? string.Empty));
        }

        //TODO: Get issuer and audience from configuration
        var token = new JwtSecurityToken(
            issuer: "DotnetApiTemplate",
            audience: "DotnetApiTemplate",
            claims: claims,
            expires: DateTime.Now.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}