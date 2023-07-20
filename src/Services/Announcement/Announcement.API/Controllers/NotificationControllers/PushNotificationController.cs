using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Announcement.Application.Features.PushNotification;

namespace Announcement.API.Controllers.NotificationControllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PushNotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PushNotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Notify")]
        public async Task<IActionResult> Notify([FromBody] PushNotificationRequest pushNotificationRequest)
        {
            var resp = await _mediator.Send(pushNotificationRequest);
            return Ok(resp);
        }
    }
}
