using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Entities;

namespace Tasks.Application.Contracts.Context
{
    public interface ITasksContext
    {
        IMongoCollection<TasksEntity> TasksCollection { get;  }
    }
}
