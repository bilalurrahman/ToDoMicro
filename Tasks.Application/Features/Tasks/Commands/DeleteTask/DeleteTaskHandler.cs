using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Tasks.Application.Contracts;

namespace Tasks.Application.Features.Tasks.Commands.DeleteTask
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTasksRequest, DeleteTaskResponse>
    {
        private readonly ITasksCommandsRepository _tasksCommandsRepository;
        private readonly IDistributedCache _distributedCache;

        public DeleteTaskHandler(ITasksCommandsRepository tasksCommandsRepository, IDistributedCache distributedCache)
        {
            _tasksCommandsRepository = tasksCommandsRepository;
            _distributedCache = distributedCache;
        }
        public async Task<DeleteTaskResponse> Handle(DeleteTasksRequest request, CancellationToken cancellationToken)
        {

            var response = await _tasksCommandsRepository.DeleteTask(request.Id);
            if(response) await _distributedCache.RemoveAsync(request.Id);
            return new DeleteTaskResponse
            {
                isSuccess = response
            };
        }
    }
}
