using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Application.Contracts.Persistance;
using MediatR;
using SharedKernal.Common.HttpContextHelper;

namespace Authentication.Application.Features.NotificationDevices.Command
{
    public class InsertUserDevicesHandler : IRequestHandler<InsertUserDevicesRequest, InsertUserDevicesResponse>
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IHttpContextHelper _httpContextHelper;
        public InsertUserDevicesHandler(IUserCommandRepository userCommandRepository, 
            IHttpContextHelper httpContextHelper)
        {
            _userCommandRepository = userCommandRepository;
            _httpContextHelper = httpContextHelper;
        }
        public async Task<InsertUserDevicesResponse> Handle(InsertUserDevicesRequest request, CancellationToken cancellationToken)
        {

            var response = await _userCommandRepository.InsertUserDevice(new Domain.Entities.UserNotificationDevices
            {
                device_token = request.device_token,
                user_id = int.Parse(_httpContextHelper.CurrentLoggedInId)
            });

            return new InsertUserDevicesResponse { 
            isSuccess = response};
        }
    }
}
