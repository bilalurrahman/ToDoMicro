
using Authentication.Application.Features.Login;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Authentication.Application.Features.Register.Commands.AddUser;

namespace Authentication.API.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
     
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest credential)
        {
            var response = await _mediator.Send(credential);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest newUser)
        {
            var response = await _mediator.Send(newUser);
            return Ok(response);
        }


    }
}
