using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pomodoros.Application.Contracts.Persistance.Command;

namespace Pomodoros.Application.Features.Pomodoros.Commad.Update
{
    public class UpdatePomodoroHandler : IRequestHandler<UpdatePomodoroRequest, UpdatePomodoroResponse>
    {
        private readonly ICommandPomodorosRepository _commandPomodorosRepository;

        public UpdatePomodoroHandler(ICommandPomodorosRepository commandPomodorosRepository)
        {
            _commandPomodorosRepository = commandPomodorosRepository;
        }
        public async Task<UpdatePomodoroResponse> Handle(UpdatePomodoroRequest request, CancellationToken cancellationToken)
        {

            var response = await _commandPomodorosRepository
                 .Update(new Domain.Entities.PomodorosEntity
                 {
                     Duration=25,
                     EndTime = request.EndTime,
                     StartTime = request.StartTime,
                     Id = request.Id,
                     TaskId = request.TaskId
                 });

            return new UpdatePomodoroResponse { isSuccess = response };
        }
    }
}
