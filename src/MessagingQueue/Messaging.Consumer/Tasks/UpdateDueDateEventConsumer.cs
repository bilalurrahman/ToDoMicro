using EventsBus.Messages.Events.Tasks;
using MassTransit;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.Extensions.Logging;

using SharedKernal.Core.Interfaces.RestClient;
using EventBus.Core.Models;

namespace EventBus.Consumer.Tasks
{
    public class UpdateDueDateEventConsumer : IConsumer<UpdateTasksDueDateEvent>
    {

        private readonly ILogger<UpdateDueDateEventConsumer> _logger;
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        protected string _updateTaskUrl => "http://tasks.api/Tasks/";

        public UpdateDueDateEventConsumer(ILogger<UpdateDueDateEventConsumer> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UpdateTasksDueDateEvent> context)
        {
            var updateTaskRepoRequest = _mapper.Map<UpdateTaskRequestModel>(context?.Message);
          
            var response = await _restClient.PutAsync<string, UpdateTaskRequestModel>(_updateTaskUrl, updateTaskRepoRequest);


            _logger.LogInformation("Due Date Updated Succeffully ");
        }
    }
}
