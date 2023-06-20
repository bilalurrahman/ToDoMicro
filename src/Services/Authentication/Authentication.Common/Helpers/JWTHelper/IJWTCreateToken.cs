
using System.Threading.Tasks;

namespace Authentication.Common.Helpers.JWTHelper
{
    public interface IJWTCreateToken
    {
        Task<JWTModel> Generate(string username, int id);
    }
}
