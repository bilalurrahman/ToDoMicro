using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pomodoros.Application.Contracts.Persistance.Command;
using Pomodoros.Application.Contracts.Persistance.Query;
using SharedKernal.Core.Enums;
using SharedKernal.Core.Interfaces.AppSettings;

namespace Pomodoros.Application.Features.Pomodoros.Commad.Insert
{
    public class InsertPomodoroHandler : IRequestHandler<InsertPomodoroRequest, InsertPomodoroResponse>
    {
        private readonly ICommandPomodorosRepository _commandPomodorosRepository;
        private readonly IAppSettingsQueryRepository _appSettingsQueryRepository;

        public InsertPomodoroHandler(ICommandPomodorosRepository commandPomodorosRepository, IAppSettingsQueryRepository appSettingsQueryRepository)
        {
            _commandPomodorosRepository = commandPomodorosRepository;
            _appSettingsQueryRepository = appSettingsQueryRepository;
        }

        public async Task<InsertPomodoroResponse> Handle(InsertPomodoroRequest request, CancellationToken cancellationToken)
        {
            var getPomodoroDuration = await _appSettingsQueryRepository
                .GetAppSettingAsync((int)AppSettingEnums.PomodoroTimeLimit);

            return new InsertPomodoroResponse
            {
                Id = await _commandPomodorosRepository
                .Add(new Domain.Entities.PomodorosEntity
                {
                    TaskId = request.TaskId,
                    Duration = int.Parse(getPomodoroDuration.AppSettingValue),
                    EndTime = request.EndTime,
                    StartTime = request.StartTime
                })
            };

            
        }
    }
}
