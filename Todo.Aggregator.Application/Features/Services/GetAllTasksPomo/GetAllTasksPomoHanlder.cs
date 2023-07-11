using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Aggregator.Application.Contracts.Integration;
using Todo.Aggregator.Application.Features.Services.GetAllTasksPomo;
using Todo.Aggregator.Application.Models;

namespace Todo.Aggregator.Application.Features.Services
{
    public class GetAllTasksPomoHanlder : IRequestHandler<GetAllTaskPomoRequest, List<GetAllTaskPomoResponse>>
    {
        private readonly IPomodoroIntegration _pomodoroIntegration;
        private readonly ITasksIntegration _tasksIntegration;

        public GetAllTasksPomoHanlder(ITasksIntegration tasksIntegration, IPomodoroIntegration pomodoroIntegration)
        {
            _tasksIntegration = tasksIntegration;
            _pomodoroIntegration = pomodoroIntegration;
        }

        public async Task<List<GetAllTaskPomoResponse>> Handle(GetAllTaskPomoRequest request, CancellationToken cancellationToken)
        {
            var getAllTaskPomoResponses = new List<GetAllTaskPomoResponse>();

            var response = await _tasksIntegration.GetAllTasks();


            List<GetAllTaskPomoResponse> destinationList = 
                response.Select(sourceItem => new GetAllTaskPomoResponse
            {
                Id = sourceItem.Id,
                CreatedBy = sourceItem.CreatedBy,
                LastModifiedBy = sourceItem.LastModifiedBy,
                CreatedDate = sourceItem.CreatedDate,
                LastModifiedDate = sourceItem.LastModifiedDate,
                isActive = sourceItem.isActive,
                userId = sourceItem.userId,
                isCompleted = sourceItem.isCompleted,
                isDeleted = sourceItem.isDeleted,

                Title = sourceItem.Title,
                Description = sourceItem.Description,
                Status = sourceItem.Status,
                DueDate = sourceItem.DueDate,
                HaveReminder = sourceItem.HaveReminder,
                ReminderDateTime = sourceItem.ReminderDateTime,
                isNotifiedForReminder = sourceItem.isNotifiedForReminder,
                isPinned = sourceItem.isPinned,
                isNotifiedForDue = sourceItem.isNotifiedForDue,
                SubTasks = sourceItem.SubTasks != null ? new List<SubTasks>(sourceItem.SubTasks) : null,
                Pomodoros = _pomodoroIntegration.GetPomodoros(sourceItem.Id).Result.ToList()
            }).ToList();


            return destinationList;
        }
    }
}
