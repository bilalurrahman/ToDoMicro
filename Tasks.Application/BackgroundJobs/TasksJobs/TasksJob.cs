using AutoMapper;
using EventsBus.Messages.Events.Tasks;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Application.Contracts;
using Tasks.Domain.Entities;

namespace Tasks.Application.BackgroundJobs.TasksJobs
{
    public class TasksJob : ITaskJob
    {
        private readonly ITasksQueryRepository _tasksQueryRepository;
        private readonly IBus _ibus;
        private readonly IMapper _imapper;

        public TasksJob(ITasksQueryRepository tasksQueryRepository, IMapper imapper, IBus ibus)
        {
            _tasksQueryRepository = tasksQueryRepository;
            _imapper = imapper;
            _ibus = ibus;
        }
        public async Task<List<TasksEntity>> DueDateCheck()
        {
            var response = await _tasksQueryRepository.GetAllForJob(); 


            //update the tasks that are already notified. 
            foreach(var resp in response)
            {
                var publishUpdateDueRequest = _imapper.Map<UpdateTasksDueDateEvent>(resp);
                publishUpdateDueRequest.isNotifiedForDue = true;
                await _ibus.Publish(publishUpdateDueRequest);

                var publishDueDateNotificationRequest = new DueDateNotificationEvent
                {
                    userDetails = new userDetails
                    {
                        userId = Convert.ToInt32(resp.userId),
                    },
                    dueDate = resp.DueDate,
                    title = resp.Title
                };
                await _ibus.Publish(publishDueDateNotificationRequest);
            }
            //two rabbits...
            //hit the rabbit after publish the message.

            return response;
        }

        public async Task<List<TasksEntity>> NextDueDateCheck()
        {
            var response = await _tasksQueryRepository.GetAllForReminderJob();

            foreach (var resp in response)
            {
                var publishNextDueRequest = _imapper.Map<UpdateTaskNextDueDateEvent>(resp);

                if (resp?.IsRepeat == true && resp?.NextDueDateForRepeat == DateTime.Now)
                {
                    resp.DueDate = (DateTime)resp?.NextDueDateForRepeat;
                }

                await _ibus.Publish(publishNextDueRequest);
            }

            return response;
        }

        public async Task<List<TasksEntity>> ReminderCheck()
        {
            var response = await _tasksQueryRepository.GetAllForReminderJob();
        
            foreach (var resp in response)
            {
                var publishUpdateDueRequest = _imapper.Map<UpdateTasksReminderDateEvent>(resp);
                publishUpdateDueRequest.isNotifiedForReminder = true;
                publishUpdateDueRequest.HaveReminder = false;
                await _ibus.Publish(publishUpdateDueRequest);

                var taskReminderNotificationEvent = new TaskReminderNotificationEvent
                {
                    userDetails = new userDetails
                    {
                        userId = Convert.ToInt32(resp.userId),
                    },
                    ReminderdDate = resp.ReminderDateTime,
                    title = resp.Title
                };
                await _ibus.Publish(taskReminderNotificationEvent);
            }
           
            return response;
        }
    }
}
