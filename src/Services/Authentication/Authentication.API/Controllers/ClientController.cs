

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;

using Microsoft.AspNetCore.Http;
using Authentication.Application.Features.Client.Login;

namespace Authentication.API.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ClientController : ControllerBase
    {
     
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost("ClientLogin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login([FromBody] ClientLoginRequest credential)
        {
            var response = await _mediator.Send(credential);
            return Ok(response);
        }

    }
}
