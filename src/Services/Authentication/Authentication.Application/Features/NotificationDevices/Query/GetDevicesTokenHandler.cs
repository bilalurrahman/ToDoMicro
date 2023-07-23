using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Application.Contracts.Persistance;
using MediatR;
namespace Authentication.Application.Features.NotificationDevices.Query
{
    public class GetDevicesTokenHandler : IRequestHandler<GetDevicesTokenRequest, List<GetDevicesTokenResponse>>
    {
        private readonly IDeviceQueryRepository deviceQueryRepository;
        public GetDevicesTokenHandler(IDeviceQueryRepository deviceQueryRepository)
        {
            this.deviceQueryRepository = deviceQueryRepository;
        }
        public async Task<List<GetDevicesTokenResponse>> Handle(GetDevicesTokenRequest request, CancellationToken cancellationToken)
        {
            var source = await deviceQueryRepository.getUserDevices(request.userId);

            List<GetDevicesTokenResponse> dest = source.Select(x =>
            new GetDevicesTokenResponse
            {
                device_token = x.device_token,
                id = x.id,
                user_id = x.user_id
            }).ToList();

            return dest;
        }
    }
}
