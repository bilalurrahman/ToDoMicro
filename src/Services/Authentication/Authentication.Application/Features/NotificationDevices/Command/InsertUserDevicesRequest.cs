using Authentication.Domain.Entities;
using MediatR;
namespace Authentication.Application.Features.NotificationDevices.Command
{
    public class InsertUserDevicesRequest: UserNotificationDevices,IRequest<InsertUserDevicesResponse>
    {

    }
}
