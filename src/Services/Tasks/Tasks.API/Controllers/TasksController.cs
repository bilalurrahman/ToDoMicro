using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Application.Features.Tasks.Commands.InsertTasks;

namespace Tasks.API.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsertTasksRequest task)
        {
            var response = await _mediator.Send(task);
            return Ok(response);
        }
    }
}
