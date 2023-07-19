using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using Authentication.Application.Features.NotificationDevices.Command;

namespace Authentication.API.Controllers
{

    public class UserDeviceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserDeviceController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsertUserDevicesRequest credential)
        {
            var response = await _mediator.Send(credential);
            return Ok(response);
        }

    }
}
