using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SharedKernal.Common.FaultTolerance;
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
        private readonly ILogger<TasksQueryRepository> logger;
        public TasksQueryRepository(ITasksContext context, ILogger<TasksQueryRepository> logger)
        {
            _context = context;
            this.logger = logger;
        }

        private async Task<T> ExecuteWithFaultPolicy<T>(Func<Task<T>> action)
        {
            return await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(action);
        }

        public async Task<TasksEntity> Get(string id)
        {
            return await ExecuteWithFaultPolicy(async () => 
            await _context.TasksCollection.Find(p => p.Id == id).FirstOrDefaultAsync());
      
        }

        public async Task<List<TasksEntity>> GetAll(long userId)
        {
            return await ExecuteWithFaultPolicy(async () =>
           await _context.TasksCollection.Find(p => p.userId == userId).ToListAsync());
                
        }

        public async Task<List<TasksEntity>> GetAllForJob()
        {
            return await ExecuteWithFaultPolicy(async () =>
            await _context.TasksCollection.Find(p => p.isCompleted == false &&
            p.isActive &&
            p.DueDate < DateTime.Now &&
            !p.isNotifiedForDue).ToListAsync());

        
        }

        public async Task<List<TasksEntity>> GetAllForReminderJob()
        {
            return await ExecuteWithFaultPolicy(async () =>
                await _context.TasksCollection.Find(p => p.isCompleted == false && 
                p.isActive && 
                p.ReminderDateTime < DateTime.Now && 
                !p.isNotifiedForReminder).ToListAsync());
        }
    }
}
