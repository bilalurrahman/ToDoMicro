using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Pomodoros.Application.Features.Pomodoros.Commad.Insert;
using Pomodoros.Application.Features.Pomodoros.Commad.Get;
using Pomodoros.Application.Features.Pomodoros.Commad.Update;
using Pomodoros.Application.Features.Pomodoros.Commad.GetAll;

namespace Pomodoros.API.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PomodoroController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PomodoroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsertPomodoroRequest pomodoro)
        {
            var response = await _mediator.Send(pomodoro);
            return Ok(response);
        }

        [HttpGet("GetAll/{TaskId}")]
        
        public async Task<IActionResult> GetAll(string TaskId)
        {

            var response = await _mediator.Send(new GetAllPomodoroRequest {  TaskId= TaskId });
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {

            var response = await _mediator.Send(new GetPomodoroRequest { Id = id });
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePomodoroRequest pomodoro)
        {
            var response = await _mediator.Send(pomodoro);
            return Ok(response);
        }
    }
}
