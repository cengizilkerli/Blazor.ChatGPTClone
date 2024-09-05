using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.Jwt;
using ChatGPTClone.Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatGPTClone.Infrastructure.Services;

public class JwtManager : IJwtService
{
    private readonly JwtSettings _jwtSettings;

    public JwtManager(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public JwtGenerateTokenResponse GenerateToken(JwtGenerateTokenRequest request)
    {
        var expiretionInMinutes = _jwtSettings.AccessTokenExpiration;
        var expiretionDate = DateTime.Now.AddMinutes(expiretionInMinutes.Minutes);

        var claims = new List<Claim>
        {
            new Claim("uid", request.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, request.Email),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss,_jwtSettings.Issuer),
            new Claim(JwtRegisteredClaimNames.Aud,_jwtSettings.Audience),
            new Claim(JwtRegisteredClaimNames.Exp, expiretionDate.ToFileTimeUtc().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToFileTimeUtc().ToString()),
             new Claim("reklam", "En iyi akademi Reklam Academy! Just joking it's the god damn Yazilim Academy!"),
        }
        .Union(request.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        var signingCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiretionDate,
            signingCredentials: signingCredential
            );

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

        return new JwtGenerateTokenResponse(token, expiretionDate);
    }
}