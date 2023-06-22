using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Tasks.Application.Contracts;
using Tasks.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using AutoMapper;

namespace Tasks.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskRequest, UpdateTaskResponse>
    {
        private readonly ITasksCommandsRepository _tasksCommandsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;
        public UpdateTaskHandler(ITasksCommandsRepository tasksCommandsRepository,
            IHttpContextAccessor httpContextAccessor, IDistributedCache distributedCache, IMapper mapper)
        {
            _tasksCommandsRepository = tasksCommandsRepository;
            _httpContextAccessor = httpContextAccessor;
            _distributedCache = distributedCache;
            _mapper = mapper;
        }
        public async Task<UpdateTaskResponse> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId").Value;
            var updateTaskRepoRequest = _mapper.Map<TasksEntity>(request);
            updateTaskRepoRequest.userId = Convert.ToInt64(userId);
            var response = await _tasksCommandsRepository.UpdateTask(updateTaskRepoRequest);
            if (response)
            {
                await _distributedCache.SetStringAsync(request.Id, JsonConvert.SerializeObject(updateTaskRepoRequest));

            }
            return new UpdateTaskResponse
            {
                isSuccess = response
            };
        }
    }
}
