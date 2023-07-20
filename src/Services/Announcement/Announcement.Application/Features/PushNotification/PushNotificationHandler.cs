using Announcement.Application.Contracts.Integration;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Announcement.Application.Features.PushNotification
{
    public class PushNotificationHandler : IRequestHandler<PushNotificationRequest, PushNotificationResponse>
    {
        private readonly IFireBaseIntegration fireBaseIntegration;

        public PushNotificationHandler(IFireBaseIntegration fireBaseIntegration)
        {
            this.fireBaseIntegration = fireBaseIntegration;
        }

        public async Task<PushNotificationResponse> Handle(PushNotificationRequest request, CancellationToken cancellationToken)
        {
            var tempDeviceToken = "eNpbQSCfkKT3_8F24lVkQa:APA91bGy_9uWr4GEq1fECip7OubmJHEjLIqecYEzD8azY3Lku5LJpWD0baVnKY1VBzzIMIFlpsfBAuEhyMg5G3pFnK9kRSOiCm54UH01AJzRgBsM6msPJ12z_DoNP5ls_fdBWX-y7WTb";
            //get from db or httpcontext.

            var resp = await fireBaseIntegration.SendNotification(tempDeviceToken, request.Title, request.Description);

            return new PushNotificationResponse {
                isSuccess = resp
            };
        }
    }
}
