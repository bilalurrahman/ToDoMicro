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
    public class TasksCommandRepository : ITasksCommandsRepository
    {
        private readonly ITasksContext _context;
        public TasksCommandRepository(ITasksContext context)
        {
            _context = context;
        }
        public async  Task CreateTask(TasksEntity tasks)
        {
            await _context.TasksCollection.InsertOneAsync(tasks);
        }

        public async Task<bool> DeleteTask(string Id)
        {
            FilterDefinition<TasksEntity> filter = Builders<TasksEntity>.Filter.Eq(p => p.Id, Id);

            DeleteResult deleteResult = await _context.TasksCollection.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<bool> UpdateTask(TasksEntity tasks)
        {
            var updateResult = await _context
                .TasksCollection
                .ReplaceOneAsync(filter: g => g.Id == tasks.Id, replacement: tasks);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }
    }
}
