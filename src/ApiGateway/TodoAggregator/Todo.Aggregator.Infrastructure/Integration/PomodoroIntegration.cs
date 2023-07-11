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
    public class PomodoroIntegration : IPomodoroIntegration
    {
        private readonly IRestClient _restClient;
        private readonly IConfiguration _config;
        public PomodoroIntegration(IRestClient restClient
            , IConfiguration config)
        {
            _restClient = restClient;
            _config = config;
        }

        public async Task<List<Pomodoros>> GetPomodoros(string TaskId)
        {
            string _getAllPomodorosUrl = _config["TodoAggregatorConfig:PomodoroUrl"] + "Pomodoro/GetAll/{0}";

            return await _restClient.GetAsync<List<Pomodoros>>
                (string.Format(_getAllPomodorosUrl,TaskId));
        }
    }
}
