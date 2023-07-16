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
        private readonly ILogger<TasksContext> Ilogger;
        public TasksContext(IOptions<NoSqlDataBaseSettings> ioptions, 
            ILogger<TasksContext> ilogger)
        {
            _ioptions = ioptions;               
            Ilogger = ilogger;

            var client = new MongoClient(_ioptions.Value.ConnectionString);
            var database = client.GetDatabase(_ioptions.Value.DBName);
            TasksCollection = 
            database.GetCollection<TasksEntity>(_ioptions.Value.CollectionName);
            InitializeTasksCollection();
        }
        private void InitializeTasksCollection()
        {
            Resiliance.mongoDbFaultPolicy(Ilogger).Result.ExecuteAsync(async () =>
            {
                try
                {
                    var client = new MongoClient(_ioptions.Value.ConnectionString);
                    var database = client.GetDatabase(_ioptions.Value.DBName);
                    TasksCollection = database.GetCollection<TasksEntity>(_ioptions.Value.CollectionName);
                }
                catch (MongoException)
                {
                    // Connection failed, retry will be attempted
                }
            }).Wait(); 
        }

        public IMongoCollection<TasksEntity> TasksCollection { get; private set; }
    }
}
