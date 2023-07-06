using MongoDB.Driver;
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
    public class TasksQueryRepository : ITasksQueryRepository
    {
        private readonly ITasksContext _context;
        public TasksQueryRepository(ITasksContext context)
        {
            _context = context;
        }

        public async Task<TasksEntity> Get(string id)
        {
            return await _context.TasksCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TasksEntity>> GetAll(long userId)
        {
            return await _context.TasksCollection.Find(p=> p.userId==userId).ToListAsync();
        }

        public async Task<List<TasksEntity>> GetAllForJob()
        {
            return await _context.TasksCollection.Find(p => p.isCompleted==false && p.isActive && p.DueDate<DateTime.Now && !p.isNotifiedForDue).ToListAsync();
        }

        public async Task<List<TasksEntity>> GetAllForReminderJob()
        {
            return await _context.TasksCollection.Find(p => p.isCompleted == false && p.isActive && p.DueDate < DateTime.Now && !p.isNotifiedForReminder).ToListAsync();
        }
    }
}
