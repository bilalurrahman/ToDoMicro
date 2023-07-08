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
    public class DueDateNotificationEventConsumer : IConsumer<DueDateNotificationEvent>
    {
        private readonly IRestClient _restClient;
        protected string _sendMail => "http://announcement.api/Email/SendEmail";
        private readonly ILogger<DueDateNotificationEventConsumer> _logger;
        public DueDateNotificationEventConsumer(ILogger<DueDateNotificationEventConsumer> logger, IRestClient restClient)
        {
           
            _logger = logger;
            _restClient = restClient;
        }
        public async Task Consume(ConsumeContext<DueDateNotificationEvent> context)
        {
            var request = new EmailClientModel
            {
                Body = $"Tasks has been Reached to the Due Date:: {context?.Message?.dueDate}",
                Subject = $"Task with Title: {context?.Message?.title} has been reached to the due date",
                ToEmail = context?.Message?.userDetails?.email
            };

            var response = await _restClient.PostAsync<string, EmailClientModel>(_sendMail, request);

            _logger.LogInformation("DueDateNotificationEventConsumer consumed successfully");

        }
    }
}
