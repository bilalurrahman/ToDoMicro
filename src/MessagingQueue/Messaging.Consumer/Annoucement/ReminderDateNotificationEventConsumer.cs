
using EventBus.Core.Models;
using EventsBus.Messages.Events.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using SharedKernal.Core.Interfaces.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Consumer.Annoucement
{
    public class ReminderDateNotificationEventConsumer : IConsumer<TaskReminderNotificationEvent>
    {
        private readonly IRestClient _restClient;
        protected string _sendMail => "http://localhost:5703/SendEmail";

        private readonly ILogger<ReminderDateNotificationEventConsumer> _logger;
        public ReminderDateNotificationEventConsumer(ILogger<ReminderDateNotificationEventConsumer> logger, IRestClient restClient)
        {

            _logger = logger;
            _restClient = restClient;
        }
        public async Task Consume(ConsumeContext<TaskReminderNotificationEvent> context)
        {

            var request = new EmailClientModel
            {
                Body = $"Reminder for the Task: {context?.Message?.ReminderdDate}",
                Subject = $"Task with Title: {context?.Message?.title} has a reminder",
                ToEmail = context?.Message?.userDetails?.email
            };

            var response = await _restClient.PostAsync<string, EmailClientModel>(_sendMail, request);


            _logger.LogInformation("ReminderDateNotificationEventConsumer consumed successfully");

        }
    }
}
