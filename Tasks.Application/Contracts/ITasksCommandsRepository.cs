using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Entities;

namespace Tasks.Application.Contracts
{
   public interface ITasksCommandsRepository
    {
        Task CreateTasks(TasksEntity tasks);
        Task UpdateTasks(TasksEntity tasks);
    }
}
