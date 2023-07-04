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
        private readonly ICachedAppSettingServices _iCachedappSettings;

        public InsertPomodoroHandler(ICommandPomodorosRepository commandPomodorosRepository,
            ICachedAppSettingServices iCachedappSettings)
        {
            _commandPomodorosRepository = commandPomodorosRepository;
            _iCachedappSettings = iCachedappSettings;
        }

        public async Task<InsertPomodoroResponse> Handle(InsertPomodoroRequest request,
            CancellationToken cancellationToken)
        {
            var getPomodoroDuration = _iCachedappSettings
                .appSettingValue((int)AppSettingEnums.PomodoroTimeLimit);

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
