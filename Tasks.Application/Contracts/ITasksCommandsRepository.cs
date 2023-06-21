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
        Task CreateTask(TasksEntity tasks);
        Task<bool> UpdateTask(TasksEntity tasks);

        Task<bool> DeleteTask(string Id);
    }
}
