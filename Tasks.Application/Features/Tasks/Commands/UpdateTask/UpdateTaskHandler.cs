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
using SharedKernal.Common.HttpContextHelper;
using SharedKernal.Common.Exceptions;

namespace Tasks.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskRequest, UpdateTaskResponse>
    {
        private readonly ITasksCommandsRepository _tasksCommandsRepository;
        private readonly IHttpContextHelper _httpContextHelper;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;
        public UpdateTaskHandler(ITasksCommandsRepository tasksCommandsRepository,
             IDistributedCache distributedCache, IMapper mapper, 
             IHttpContextHelper httpContextHelper)
        {
            _tasksCommandsRepository = tasksCommandsRepository;
            
            _distributedCache = distributedCache;
            _mapper = mapper;
            _httpContextHelper = httpContextHelper;
        }
        public async Task<UpdateTaskResponse> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
        {

            var userId = (request.userId > 0) ? request.userId.ToString()
                : _httpContextHelper.CurrentLoggedInId;

            if (string.IsNullOrEmpty(request?.Title))
                throw new BusinessRuleException(LogEventIds.BusinessRuleEventIds.TitleCantBeEmpty.Id, LogEventIds.BusinessRuleEventIds.TitleCantBeEmpty.Name);


            //get the older values 
            //match the due date and reminder date and set the values of isnotified due to false and isnotified reminder to false.


            var updateTaskRepoRequest = _mapper.Map<TasksEntity>(request);
            updateTaskRepoRequest.userId = Convert.ToInt64(userId);
            var response = await _tasksCommandsRepository.UpdateTask(updateTaskRepoRequest);
            if (!response)
                throw new EntityNotFoundException(LogEventIds.EntityNotFoundEventIds.TaskIdNotFound.Id,
                       LogEventIds.EntityNotFoundEventIds.TaskIdNotFound.Name);

            await _distributedCache.SetStringAsync(request.Id, JsonConvert.SerializeObject(updateTaskRepoRequest));

            
            return new UpdateTaskResponse
            {
                isSuccess = response
            };
        }
    }
}
