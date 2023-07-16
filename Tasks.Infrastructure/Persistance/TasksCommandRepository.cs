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
    public class TasksCommandRepository : ITasksCommandsRepository
    {
        private readonly ITasksContext _context;
        private readonly ILogger<TasksCommandRepository> logger;
        public TasksCommandRepository(ITasksContext context, ILogger<TasksCommandRepository> logger)
        {
            _context = context;
            this.logger = logger;
        }

        private async Task<T> ExecuteWithFaultPolicy<T>(Func<Task<T>> action)
        {
            return await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(action);
        }
        public async  Task<string> CreateTask(TasksEntity tasks)
        {
             await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(async () => await _context.TasksCollection.InsertOneAsync(tasks));
            return tasks.Id;
        }

        public async Task<bool> DeleteTask(string Id)
        {
            FilterDefinition<TasksEntity> filter = Builders<TasksEntity>.Filter.Eq(p => p.Id, Id);

            DeleteResult deleteResult = await 
                ExecuteWithFaultPolicy(async () => await _context.TasksCollection.DeleteOneAsync(filter));

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<bool> UpdateTask(TasksEntity tasks)
        {
            var updateResult = await ExecuteWithFaultPolicy(async () => await _context
                .TasksCollection
                .ReplaceOneAsync(filter: g => g.Id == tasks.Id, replacement: tasks));

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }
    }
}
