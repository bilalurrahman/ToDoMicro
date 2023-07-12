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
using Tasks.Application.Features.Tasks.Queries.GetTask;

namespace Tasks.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskRequest, UpdateTaskResponse>
    {
        private readonly ITasksCommandsRepository _tasksCommandsRepository;
        private readonly ITasksQueryRepository _taskQueryRepository;
        private readonly IHttpContextHelper _httpContextHelper;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;

        public UpdateTaskHandler(ITasksCommandsRepository tasksCommandsRepository,
             IDistributedCache distributedCache, IMapper mapper,
             IHttpContextHelper httpContextHelper
, ITasksQueryRepository taskQueryRepository)
        {
            _tasksCommandsRepository = tasksCommandsRepository;

            _distributedCache = distributedCache;
            _mapper = mapper;
            _httpContextHelper = httpContextHelper;
            _taskQueryRepository = taskQueryRepository;
        }
        public async Task<UpdateTaskResponse> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
        {

            var userId = (request.userId > 0) ? request.userId.ToString()
                : _httpContextHelper.CurrentLoggedInId;

            if (string.IsNullOrEmpty(request?.Title))
                throw new BusinessRuleException(LogEventIds.BusinessRuleEventIds.TitleCantBeEmpty.Id, LogEventIds.BusinessRuleEventIds.TitleCantBeEmpty.Name);


            //get the older values
            await DomainBusiness(request, userId);
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

        private async Task DomainBusiness(UpdateTaskRequest request, string userId)
        {
            var getOlderVals = await _taskQueryRepository.Get(request.Id); //get the value from cache?

            ////match the due date and reminder date and set the values of isnotified due to false and isnotified reminder to false.
            if (getOlderVals.DueDate != request.DueDate)
                request.isNotifiedForDue = false;
            if (request.HaveReminder
                && getOlderVals.ReminderDateTime != request.ReminderDateTime)
                request.isNotifiedForReminder = false;


            request.LastModifiedBy = userId;
            request.LastModifiedDate = DateTime.Now;
            if (request?.IsRepeat == true)
            {
                request.NextDueDateForRepeat =
                   request.DueDate.AddDays((double)request.RepeatFrequency);
            }

        }
    }
}
