using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SharedKernal.Common.FaultTolerance;
using Tasks.Application.Contracts.Context;
using Tasks.Application.Models;
using Tasks.Domain.Entities;

namespace Tasks.Infrastructure.Context
{
    public class TasksContext : ITasksContext
    {
        private readonly IOptions<NoSqlDataBaseSettings> _ioptions;

        public TasksContext(IOptions<NoSqlDataBaseSettings> ioptions)
        {
            _ioptions = ioptions;               
           

            var client = new MongoClient(_ioptions.Value.ConnectionString);
            var database = client.GetDatabase(_ioptions.Value.DBName);
            TasksCollection = 
            database.GetCollection<TasksEntity>(_ioptions.Value.CollectionName);          
        }
       
        public IMongoCollection<TasksEntity> TasksCollection { get; private set; }
    }
}
