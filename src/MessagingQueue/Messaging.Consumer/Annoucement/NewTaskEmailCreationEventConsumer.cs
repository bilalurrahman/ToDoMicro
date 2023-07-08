
using EventBus.Core.Models;
using EventsBus.Messages.Events.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using SharedKernal.Core.Interfaces.RestClient;
using System.Threading.Tasks;

namespace EventBus.Consumer.Annoucement
{
    public class NewTaskEmailCreationEventConsumer : IConsumer<NewTaskEmailCreationEvent>
    {
        
        private readonly ILogger<NewTaskEmailCreationEventConsumer> _logger;
        private readonly IRestClient _restClient;
        protected string _sendMail => "http://announcement.api/Email/SendEmail";
        public NewTaskEmailCreationEventConsumer(ILogger<NewTaskEmailCreationEventConsumer> logger, IRestClient restClient)
        {
            _logger = logger;
            _restClient = restClient;
        }
        public async Task Consume(ConsumeContext<NewTaskEmailCreationEvent> context)
        {

            var request = new EmailClientModel
            {
                Body = $"New Tasks Created With Description: {context?.Message?.description} and Due Date: {context?.Message?.dueDate}",
                Subject = $"New Task Created with Title: {context?.Message?.title}",
                ToEmail = context?.Message?.userDetails?.email
            };

            var response = await _restClient.PostAsync<string, EmailClientModel>(_sendMail, request);


            _logger.LogInformation("NewTaskEmailCreationEvent consumed successfully");

        }
    }
}
