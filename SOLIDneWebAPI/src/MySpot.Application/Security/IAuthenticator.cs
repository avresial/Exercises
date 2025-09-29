using MySpot.Application.Dtos;

namespace MySpot.Application.Security;
public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}
