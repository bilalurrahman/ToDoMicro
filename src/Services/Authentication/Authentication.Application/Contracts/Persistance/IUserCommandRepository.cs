using Authentication.Domain.Entities;
using System.Threading.Tasks;

namespace Authentication.Application.Contracts.Persistance
{
   public interface IUserCommandRepository
    {        
        Task<bool> Insert(RegisterUser user);
        Task<bool> Update(RegisterUser user);

    }
}
