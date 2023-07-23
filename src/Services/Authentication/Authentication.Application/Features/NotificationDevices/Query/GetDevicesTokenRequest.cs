using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Authentication.Application.Features.NotificationDevices.Query
{
    public class GetDevicesTokenRequest:IRequest<List<GetDevicesTokenResponse>>
    {
        public int userId { get; set; }
    }
}
