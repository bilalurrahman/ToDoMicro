using Microsoft.Extensions.Configuration;
using SharedKernal.Core.Interfaces.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Aggregator.Application.Contracts.Integration;
using Todo.Aggregator.Application.Models;

namespace Todo.Aggregator.Infrastructure.Integration
{
    public class TaskIntegration : ITasksIntegration
    {
       //get from configuration
        private readonly IRestClient _restClient;
        private readonly IConfiguration _config;
        public TaskIntegration(IRestClient restClient, IConfiguration config)
        {
            _restClient = restClient;
            _config = config;
        }

        public async Task<List<PomoTaskModel>> GetAllTasks()
        {
            string _getAllTasksUrl = _config["TodoAggregatorConfig:TasksUrl"]+"/Tasks";
            return await _restClient.GetAsync<List<PomoTaskModel>>(_getAllTasksUrl);            
        }
    }
}
