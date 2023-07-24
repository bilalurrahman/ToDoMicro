using Announcement.Application.Contracts.Integration;
using Announcement.Application.Models;
using MediatR;
using SharedKernal.Core.Interfaces.RestClient;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Announcement.Application.Features.PushNotification
{
    public class PushNotificationHandler : IRequestHandler<PushNotificationRequest, PushNotificationResponse>
    {
        private readonly IFireBaseIntegration fireBaseIntegration;
        private readonly IRestClient restClient;
        private readonly string _getAllUserDevices = "http://authentication.api/UserDevice?userId=";

        public PushNotificationHandler(IFireBaseIntegration fireBaseIntegration, 
            IRestClient restClient)
        {
            this.fireBaseIntegration = fireBaseIntegration;
            this.restClient = restClient;
        }

        public async Task<PushNotificationResponse> Handle(PushNotificationRequest request, CancellationToken cancellationToken)
        {
           // var tempDeviceToken = "eNpbQSCfkKT3_8F24lVkQa:APA91bGy_9uWr4GEq1fECip7OubmJHEjLIqecYEzD8azY3Lku5LJpWD0baVnKY1VBzzIMIFlpsfBAuEhyMg5G3pFnK9kRSOiCm54UH01AJzRgBsM6msPJ12z_DoNP5ls_fdBWX-y7WTb";
            //get from db or httpcontext.
            
            var devices =  await restClient.GetAsync<List<UserDevicesModel>>($"{ _getAllUserDevices}{request.userId}");
            
            foreach(var device in devices)
            {
                await fireBaseIntegration.SendNotification(device.device_token, request.Title, request.Description);
            }
            

            return new PushNotificationResponse {
                isSuccess = true
            };
        }
    }
}
