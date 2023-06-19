using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Application.Contracts;
using Tasks.Application.Contracts.Context;
using Tasks.Domain.Entities;

namespace Tasks.Infrastructure.Persistance
{
    public class TasksCommandRepository : ITasksCommandsRepository
    {
        private readonly ITasksContext _context;
        public TasksCommandRepository(ITasksContext context)
        {
            _context = context;
        }
        public async  Task CreateTasks(TasksEntity tasks)
        {
            await _context.TasksCollection.InsertOneAsync(tasks);
        }

        public Task UpdateTasks(TasksEntity tasks)
        {
            throw new NotImplementedException();
        }
    }
}
