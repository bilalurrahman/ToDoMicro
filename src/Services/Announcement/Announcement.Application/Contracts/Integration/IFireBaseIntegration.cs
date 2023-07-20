using System.Threading.Tasks;

namespace Announcement.Application.Contracts.Integration
{
    public interface IFireBaseIntegration
    {
        Task<bool> SendNotification(string token, string title, string body);
    }
}
