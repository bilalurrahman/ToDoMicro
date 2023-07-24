﻿using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using Authentication.Application.Features.NotificationDevices.Command;
using Authentication.Application.Features.NotificationDevices.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Authentication.API.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet]
        public async Task<IActionResult> GetAll(int userId)
        {
            var response = await _mediator.Send(new GetDevicesTokenRequest { 
                userId = userId
            });
            return Ok(response);
        }



    }
}
