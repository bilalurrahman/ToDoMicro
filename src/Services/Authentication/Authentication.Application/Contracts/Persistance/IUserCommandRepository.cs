using Authentication.Domain.Entities;
using System.Threading.Tasks;

namespace Authentication.Application.Contracts.Persistance
{
   public interface IUserCommandRepository
    {        
        Task<bool> InsertUser(RegisterUser user);
        Task<bool> UpdateUser(RegisterUser user);

        Task<bool> UpdateRefreshToken(UserToken userToken);

        Task<bool> InsertUserDevice(UserNotificationDevices userNotificationDevices);

    }
}
