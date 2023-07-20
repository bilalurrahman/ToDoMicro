using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Entities;

namespace Tasks.Application.Contracts
{
   public interface ITasksQueryRepository
    {
        Task<List<TasksEntity>> GetAll(long userId);
        Task <TasksEntity> Get(string id);

        Task<List<TasksEntity>> GetAllForJob();

        Task<List<TasksEntity>> GetAllForReminderJob();
        Task<List<TasksEntity>> GetAllForNextDue();

    }
}
