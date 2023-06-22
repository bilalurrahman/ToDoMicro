using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public GetTaskHandler(ITasksQueryRepository tasksQueryRepository,
            IDistributedCache distributedCache, IMapper mapper)
        {
            _tasksQueryRepository = tasksQueryRepository;
            _distributedCache = distributedCache;
            _mapper = mapper;
        }
        public async Task<GetTaskResponse> Handle(GetTaskRequest request, CancellationToken cancellationToken)
        {
            TasksEntity response = new TasksEntity();
            
            var cachedData = await _distributedCache.GetStringAsync(request.Id);
            if (!String.IsNullOrEmpty(cachedData))
            {
                response = JsonConvert.DeserializeObject<TasksEntity>(cachedData);               
            }
            else
            {
                response = await _tasksQueryRepository.Get(request.Id);
                if (response == null)
                    return new GetTaskResponse();//exception to be thrown here...

                await _distributedCache.SetStringAsync(request.Id, JsonConvert.SerializeObject(response));

            }
            return _mapper.Map<GetTaskResponse>(response);

        }
    }
}
