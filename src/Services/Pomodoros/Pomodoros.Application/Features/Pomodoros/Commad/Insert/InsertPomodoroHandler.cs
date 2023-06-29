using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pomodoros.Application.Contracts.Persistance.Command;
using Pomodoros.Application.Contracts.Persistance.Query;

namespace Pomodoros.Application.Features.Pomodoros.Commad.Insert
{
    public class InsertPomodoroHandler : IRequestHandler<InsertPomodoroRequest, InsertPomodoroResponse>
    {
        private readonly ICommandPomodorosRepository _commandPomodorosRepository;

        public InsertPomodoroHandler(ICommandPomodorosRepository commandPomodorosRepository)
        {
            _commandPomodorosRepository = commandPomodorosRepository;
        }

        public async Task<InsertPomodoroResponse> Handle(InsertPomodoroRequest request, CancellationToken cancellationToken)
        {

            await _commandPomodorosRepository
                .Add(new Domain.Entities.PomodorosEntity{ 
                    TaskId = request.TaskId,
                    Duration = 25,
                    EndTime = request.EndTime,
                    StartTime = request.StartTime
                });

            return new InsertPomodoroResponse { isSuccess = true };

            
        }
    }
}
