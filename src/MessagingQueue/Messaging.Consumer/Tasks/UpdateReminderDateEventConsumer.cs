using EventsBus.Messages.Events.Tasks;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.Extensions.Logging;

using EventBus.Core.Models;
using SharedKernal.Core.Interfaces.RestClient;

namespace EventBus.Consumer.Tasks
{
    public class UpdateReminderDateEventConsumer : IConsumer<UpdateTasksReminderDateEvent>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateReminderDateEventConsumer> _logger;
        protected string _updateTaskUrl => "http://tasks.api/Tasks/";

        public UpdateReminderDateEventConsumer(IMapper mapper, ILogger<UpdateReminderDateEventConsumer> logger, IRestClient restClient)
        {
            _mapper = mapper;
            _logger = logger;
            _restClient = restClient;
        }

        public async Task Consume(ConsumeContext<UpdateTasksReminderDateEvent> context)
        {
            var updateTaskRepoRequest = _mapper.Map<UpdateTaskRequestModel>(context?.Message);
            var response = await _restClient.PutAsync<string, UpdateTaskRequestModel>(_updateTaskUrl, updateTaskRepoRequest);

            _logger.LogInformation("Reminder Date Updated Successfully ");
        }
    }
}
