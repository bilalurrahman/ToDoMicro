using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Application.Contracts;
using Tasks.Domain.Entities;

namespace Tasks.Application.Features.Tasks.Commands.InsertTasks
{
    public class InsertTasksHandler : IRequestHandler<InsertTasksRequest, InsertTasksResponse>
    {
        private readonly ITasksCommandsRepository _tasksCommandsRepository;
        public InsertTasksHandler(ITasksCommandsRepository tasksCommandsRepository)
        {
            _tasksCommandsRepository = tasksCommandsRepository;
        }
        public async Task<InsertTasksResponse> Handle(InsertTasksRequest request, CancellationToken cancellationToken)
        {
            await _tasksCommandsRepository.CreateTasks(new TasksEntity
            {
                Title = request.Title,
                Description= request.Description,
                DueDate=request.DueDate,
                isPinned=request.isPinned,
                Status=request.Status,
                HaveReminder=request.HaveReminder,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                CreatedBy=request.CreatedBy,
                isActive=request.isActive,
                userId=request.userId,
                LastModifiedBy=request.LastModifiedBy,
                isCompleted=request.isCompleted
            });

            return new InsertTasksResponse();


        }
    }
}
