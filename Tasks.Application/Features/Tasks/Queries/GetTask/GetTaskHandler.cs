using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.Application.Contracts;

namespace Tasks.Application.Features.Tasks.Queries.GetTask
{
    public class GetTaskHandler : IRequestHandler<GetTaskRequest, GetTaskResponse>
    {
        private readonly ITasksQueryRepository _tasksQueryRepository;
        public GetTaskHandler(ITasksQueryRepository tasksQueryRepository)
        {
            _tasksQueryRepository = tasksQueryRepository;
        }
        public async Task<GetTaskResponse> Handle(GetTaskRequest request, CancellationToken cancellationToken)
        {
         
            //get from redis if id is there and modifieddate==modifieddate of redis
            //redisCache
            //else get from the repo of mongo.
            
            var response = await _tasksQueryRepository.Get(request.Id);
            

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
