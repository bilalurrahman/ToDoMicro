using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.Application.Contracts;

namespace Tasks.Application.Features.Tasks.Commands.DeleteTask
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTasksRequest, DeleteTaskResponse>
    {
        private readonly ITasksCommandsRepository _tasksCommandsRepository;
        public DeleteTaskHandler(ITasksCommandsRepository tasksCommandsRepository)
        {
            _tasksCommandsRepository = tasksCommandsRepository;
        }
        public async Task<DeleteTaskResponse> Handle(DeleteTasksRequest request, CancellationToken cancellationToken)
        {
            return new DeleteTaskResponse
            {
                isSuccess = await _tasksCommandsRepository.DeleteTask(request.Id)
            };
        }
    }
}
