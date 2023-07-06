using Announcement.Application.Contracts.Integration;
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
        public async Task<ActionResult> SendEmail()
        {
            try {
                await emailIntegration
                .SendEmailAsync(new Application.Models.MailRequest
                {
                    Body = "Test Body for To do application",
                    ToEmail = "bilal.ur.rahman2@gmail.com",
                    Subject = "Test Subject"
                });
            }
            catch(Exception ex)
            {
                return Ok("Email Not Sent");
            }

            return Ok("Email Sent");
        }
    }
}
