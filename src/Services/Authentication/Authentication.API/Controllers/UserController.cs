
using Authentication.Application.Features.Login;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
namespace Authentication.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
     
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest credential)
        {
            var response = await _mediator.Send(credential);
            return Ok(response);
        }

        

    }
}
