using Authentication.Domain.Entities;
using System.Threading.Tasks;

namespace Authentication.Application.Contracts.Persistance
{
   public interface IUserQueryRepository
    {
        Task<RegisterUser> GetUserInfo(string username);

        Task<UserToken> GetRefreshToken(string username);
    }
}
