﻿using System;
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

namespace Tasks.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskRequest, UpdateTaskResponse>
    {
        private readonly ITasksCommandsRepository _tasksCommandsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCache _distributedCache;
        public UpdateTaskHandler(ITasksCommandsRepository tasksCommandsRepository,
            IHttpContextAccessor httpContextAccessor, IDistributedCache distributedCache)
        {
            _tasksCommandsRepository = tasksCommandsRepository;
            _httpContextAccessor = httpContextAccessor;
            _distributedCache = distributedCache;
        }
        public async Task<UpdateTaskResponse> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId").Value;

            var taskRequest = new TasksEntity
            {

                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                isPinned = request.isPinned,
                Status = request.Status,
                HaveReminder = request.HaveReminder,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                CreatedBy = request.CreatedBy,
                isActive = request.isActive,
                userId = Convert.ToInt64(userId),
                LastModifiedBy = request.LastModifiedBy,
                isCompleted = request.isCompleted,
                Id = request.Id
            };
            var response = await _tasksCommandsRepository.UpdateTask(taskRequest);
            if (response)
            {
                await _distributedCache.SetStringAsync(request.Id, JsonConvert.SerializeObject(taskRequest));

            }
            return new UpdateTaskResponse
            {
                isSuccess = response
            };
        }
    }
}
