
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.Common.Helpers.JWTHelper
{
    public interface IJWTCreateToken
    {
        Task<JWTModel> GenerateToken(string username, int id);

        Task<string> GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
