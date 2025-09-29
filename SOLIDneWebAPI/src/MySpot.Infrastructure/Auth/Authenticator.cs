using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySpot.Application.Dtos;
using MySpot.Application.Security;
using MySpot.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MySpot.Infrastructure.Auth;
internal class Authenticator : IAuthenticator
{
    private readonly IClock _clock;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly TimeSpan? _expire;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    public Authenticator(IOptions<AuthOptions> options, IClock clock)
    {
        _clock = clock;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _expire = options.Value.Expiry;

        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey)),
            SecurityAlgorithms.HmacSha256);
    }
    public JwtDto CreateToken(Guid userId, string role)
    {
        var now = _clock.Current();
        var expires = now.Add(_expire.Value);
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Role, role)
        };

        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);

        var accessToken = _jwtSecurityTokenHandler.WriteToken(jwt);

        return new()
        {
            AccessToken = accessToken
        };
    }
}
