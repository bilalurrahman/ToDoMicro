using Announcement.Application.Contracts.Integration;
using Announcement.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement.API.Controllers.EmailControllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailIntegration emailIntegration;

        public EmailController(IEmailIntegration emailIntegration)
        {
            this.emailIntegration = emailIntegration;
        }

        [HttpPost("SendEmail")]
        public async Task<ActionResult> SendEmail([FromBody] MailRequest mailRequest)
        {
            try {
                await emailIntegration
                .SendEmailAsync(mailRequest);
            }
            catch(Exception ex)
            {
                return Ok("Email Not Sent");
            }

            return Ok("Email Sent");
        }
    }
}
