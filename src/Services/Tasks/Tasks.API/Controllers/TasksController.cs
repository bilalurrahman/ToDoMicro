using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tasks.Application.Features.Tasks.Commands.DeleteTask;
using Tasks.Application.Features.Tasks.Commands.InsertTasks;
using Tasks.Application.Features.Tasks.Commands.UpdateTask;
using Tasks.Application.Features.Tasks.Queries;
using Tasks.Application.Features.Tasks.Queries.GetTask;

namespace Tasks.API.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var response = await _mediator.Send(new GetAllTasksRequest { UserId = 1 });
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {

            var response = await _mediator.Send(new GetTaskRequest { Id = id });
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTaskRequest task)
        {
            var response = await _mediator.Send(task);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _mediator.Send(new DeleteTasksRequest { Id = id });
            return Ok(response);
        }
    }
}
