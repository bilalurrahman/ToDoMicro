using Announcement.Application.Contracts.Integration;
using FirebaseAdmin.Messaging;
using System;
using System.Threading.Tasks;


namespace Annoucement.Infrastructure.Integration
{
    public class FireBaseIntegration : IFireBaseIntegration
    {
      
        
        public async Task<bool> SendNotification(string token, string title, string body)
        {
            var message = new Message
            {
                Token = token,
                Notification = new Notification
                {
                    Title = title,
                    Body = body,
                }
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            await messaging.SendAsync(message);

            return true;
        }
    }
}
