using DotnetApiTemplate.Domain.Models;
using DotnetApiTemplate.Domain.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotnetApiTemplate.Services.Services.Auth;

public class JwtTokenGenerator(IUnitOfWork unitOfWork)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<string> GenerateToken(User user, string secretKey, int expirationMinutes, string issuer, string audience)
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
            claims = await AddSubRole(role, claims);
        }

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<List<Claim>> AddSubRole(Role role, List<Claim> claims)
    {

            claims.Add(new Claim(ClaimTypes.Role, role?.Name ?? string.Empty));

            if (role.SubRoles.Any())
            {
                foreach (Role subRole in role.SubRoles)
                {
                    claims = await AddSubRole((await _unitOfWork.Roles.GetAsync(r => r.Name == subRole.Name)).FirstOrDefault()!, claims);
                }
            }

            return claims;
    }
}