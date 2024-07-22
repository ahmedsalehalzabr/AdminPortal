using Microsoft.AspNetCore.Identity;

namespace AdminPortal.Repositories
{
    public interface ITokenRepository
    {
       string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
