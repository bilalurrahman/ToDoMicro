using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Application.Contracts.Context;
using Tasks.Domain.Entities;

namespace Tasks.Infrastructure.Context
{
    public class TasksContext : ITasksContext
    {

        public TasksContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DataBaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DataBaseSettings:DBName"]);
            TasksCollection = database.GetCollection<TasksEntity>(configuration["DataBaseSettings:CollectionName"]);
        }

        public IMongoCollection<TasksEntity> TasksCollection { get; }
    }
}
