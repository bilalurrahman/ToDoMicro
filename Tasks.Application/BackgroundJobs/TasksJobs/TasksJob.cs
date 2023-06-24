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
        public TasksJob(ITasksQueryRepository tasksQueryRepository)
        {
            _tasksQueryRepository = tasksQueryRepository;
        }
        public async Task<List<TasksEntity>> DueDateCheck()
        {
            var resp = await _tasksQueryRepository.GetAllForJob(); 


            //update the tasks that are already notified. 
            //two rabbits...
            //hit the rabbit after publish the message.

            return resp;
        }
    }
}
