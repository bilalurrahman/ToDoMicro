using Announcement.Application.Contracts.Integration;
using EventsBus.Messages.Events.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcement.Application.Features.MessageConsumer
{
    public class NewTaskEmailCreationEventConsumer : IConsumer<NewTaskEmailCreationEvent>
    {
        private readonly IEmailIntegration _emailIntegration;

        private readonly ILogger<NewTaskEmailCreationEventConsumer> _logger;
        public NewTaskEmailCreationEventConsumer(IEmailIntegration emailIntegration, ILogger<NewTaskEmailCreationEventConsumer> logger)
        {
            _emailIntegration = emailIntegration;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<NewTaskEmailCreationEvent> context)
        {

            await _emailIntegration.SendEmailAsync(new Models.MailRequest
            {
                Body=$"New Tasks Created With Description: {context?.Message?.description} and Due Date: {context?.Message?.dueDate}",
                Subject=$"New Task Created with Title: {context?.Message?.title}",
                ToEmail = context?.Message?.userDetails?.email
            });

            _logger.LogInformation("NewTaskEmailCreationEvent consumed successfully");

        }
    }
}
