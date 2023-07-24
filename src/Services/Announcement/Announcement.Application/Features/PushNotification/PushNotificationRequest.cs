using MediatR;
namespace Announcement.Application.Features.PushNotification
{
    public class PushNotificationRequest:IRequest<PushNotificationResponse>
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public int userId { get; set; }
    }
}
