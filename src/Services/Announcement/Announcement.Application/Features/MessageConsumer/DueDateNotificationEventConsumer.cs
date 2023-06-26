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
    public class DueDateNotificationEventConsumer : IConsumer<DueDateNotificationEvent>
    {
        private readonly IEmailIntegration _emailIntegration;

        private readonly ILogger<NewTaskEmailCreationEventConsumer> _logger;
        public DueDateNotificationEventConsumer(IEmailIntegration emailIntegration, ILogger<NewTaskEmailCreationEventConsumer> logger)
        {
            _emailIntegration = emailIntegration;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<DueDateNotificationEvent> context)
        {

            await _emailIntegration.SendEmailAsync(new Models.MailRequest
            {
                Body=$"Tasks has been Reached to the Due Date:: {context?.Message?.dueDate}",
                Subject=$"Task with Title: {context?.Message?.title} has been reached to the due date",
                ToEmail = context?.Message?.userDetails?.email
            });

            _logger.LogInformation("DueDateNotificationEventConsumer consumed successfully");

        }
    }
}
