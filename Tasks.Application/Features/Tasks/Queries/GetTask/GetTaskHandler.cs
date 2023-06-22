using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Tasks.Application.Contracts;
using Tasks.Domain.Entities;

namespace Tasks.Application.Features.Tasks.Queries.GetTask
{
    public class GetTaskHandler : IRequestHandler<GetTaskRequest, GetTaskResponse>
    {
        private readonly ITasksQueryRepository _tasksQueryRepository;
        private readonly IDistributedCache _distributedCache;
        public GetTaskHandler(ITasksQueryRepository tasksQueryRepository, 
            IDistributedCache distributedCache)
        {
            _tasksQueryRepository = tasksQueryRepository;
            _distributedCache = distributedCache;
        }
        public async Task<GetTaskResponse> Handle(GetTaskRequest request, CancellationToken cancellationToken)
        {

            //get from redis if id is there and modifieddate==modifieddate of redis
            var cachedData = await _distributedCache.GetStringAsync(request.Id);
            if (String.IsNullOrEmpty(cachedData))
            {
                var response = await _tasksQueryRepository.Get(request.Id);
                if (response == null)
                    return new GetTaskResponse();//exception to be thrown here...

                await _distributedCache.SetStringAsync(request.Id, JsonConvert.SerializeObject(response));
                return Mapper(response);
            }
            else
            {
              var response = JsonConvert.DeserializeObject<TasksEntity>(cachedData);
              return Mapper(response);
            }

        }

        private static GetTaskResponse Mapper(TasksEntity response)
        {
            return new GetTaskResponse
            {
                CreatedBy = response.CreatedBy,
                CreatedDate = response.CreatedDate,
                Description = response.Description,
                DueDate = response.DueDate,
                HaveReminder = response.HaveReminder,
                Id = response.Id,
                isActive = response.isActive,
                isPinned = response.isPinned,
                LastModifiedBy = response.LastModifiedBy,
                LastModifiedDate = response.LastModifiedDate,
                Status = response.Status,
                Title = response.Title,
                userId = response.userId,
                isCompleted = response.isCompleted
            };
        }
    }
}
