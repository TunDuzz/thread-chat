using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ThreadChat.Application.Interfaces.Services;
using ThreadChat.Domain.Entities;

namespace ThreadChat.Infrastructure.Services;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private readonly string _issuer;
    private readonly string _audience;
    private readonly TimeSpan _accessTokenLifetime;
    private readonly SymmetricSecurityKey _signingKey;

    private DateTimeOffset _lastExpiresAtUtc = DateTimeOffset.UtcNow;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;

        _issuer = _configuration["Jwt:Issuer"] ?? "ThreadChat";
        _audience = _configuration["Jwt:Audience"] ?? "ThreadChatClient";

        var minutes = int.TryParse(_configuration["Jwt:AccessTokenMinutes"], out var m) ? m : 60;
        _accessTokenLifetime = TimeSpan.FromMinutes(minutes);

        var secret = _configuration["Jwt:Secret"] ?? throw new InvalidOperationException("Jwt:Secret is not configured.");
        _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    }

    public string CreateAccessToken(User user, IReadOnlyDictionary<string, string>? extraClaims = null)
    {
        var now = DateTimeOffset.UtcNow;
        var expires = now.Add(_accessTokenLifetime);
        _lastExpiresAtUtc = expires;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.Username),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Role, user.SystemRole.ToString())
        };

        if (extraClaims is not null)
        {
            foreach (var kv in extraClaims)
            {
                claims.Add(new Claim(kv.Key, kv.Value));
            }
        }

        var credentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            notBefore: now.UtcDateTime,
            expires: expires.UtcDateTime,
            signingCredentials: credentials);

        return _tokenHandler.WriteToken(token);
    }

    public string CreateRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }

    public DateTimeOffset GetAccessTokenExpiresAtUtc() => _lastExpiresAtUtc;
}

