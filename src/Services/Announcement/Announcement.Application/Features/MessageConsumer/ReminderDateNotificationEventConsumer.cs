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
    public class ReminderDateNotificationEventConsumer : IConsumer<TaskReminderNotificationEvent>
    {
        private readonly IEmailIntegration _emailIntegration;

        private readonly ILogger<NewTaskEmailCreationEventConsumer> _logger;
        public ReminderDateNotificationEventConsumer(IEmailIntegration emailIntegration, ILogger<NewTaskEmailCreationEventConsumer> logger)
        {
            _emailIntegration = emailIntegration;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<TaskReminderNotificationEvent> context)
        {

            await _emailIntegration.SendEmailAsync(new Models.MailRequest
            {
                Body=$"Reminder for the Task: {context?.Message?.ReminderdDate}",
                Subject=$"Task with Title: {context?.Message?.title} has a reminder",
                ToEmail = context?.Message?.userDetails?.email
            });

            _logger.LogInformation("ReminderDateNotificationEventConsumer consumed successfully");

        }
    }
}
