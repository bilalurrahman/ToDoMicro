using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Application.Contracts;
using Tasks.Domain.Entities;

namespace Tasks.Infrastructure.Persistance
{
    public class TasksCommandRepository : ITasksCommandsRepository
    {
        public TasksCommandRepository()
        {

        }
        public Task CreateTasks(TasksEntity tasks)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTasks(TasksEntity tasks)
        {
            throw new NotImplementedException();
        }
    }
}
