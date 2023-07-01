using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pomodoros.Application.Contracts.Persistance.Command;
using SharedKernal.Core.Enums;
using SharedKernal.Core.Interfaces.AppSettings;

namespace Pomodoros.Application.Features.Pomodoros.Commad.Update
{
    public class UpdatePomodoroHandler : IRequestHandler<UpdatePomodoroRequest, UpdatePomodoroResponse>
    {
        private readonly ICommandPomodorosRepository _commandPomodorosRepository;
        private readonly IAppSettingsQueryRepository _appSettingsQueryRepository;
        public UpdatePomodoroHandler(ICommandPomodorosRepository commandPomodorosRepository, IAppSettingsQueryRepository appSettingsQueryRepository)
        {
            _commandPomodorosRepository = commandPomodorosRepository;
            _appSettingsQueryRepository = appSettingsQueryRepository;
        }
        public async Task<UpdatePomodoroResponse> Handle(UpdatePomodoroRequest request, CancellationToken cancellationToken)
        {
            var getPomodoroDuration = await _appSettingsQueryRepository
                .GetAppSettingAsync((int)AppSettingEnums.PomodoroTimeLimit);
            var response = await _commandPomodorosRepository
                 .Update(new Domain.Entities.PomodorosEntity
                 {
                     Duration = int.Parse(getPomodoroDuration.AppSettingValue),
                     EndTime = request.EndTime,
                     StartTime = request.StartTime,
                     Id = request.Id,
                     TaskId = request.TaskId
                 });

            return new UpdatePomodoroResponse { isSuccess = response };
        }
    }
}
