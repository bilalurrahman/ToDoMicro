using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pomodoros.Application.Contracts.Context;
using Pomodoros.Application.Models;
using Pomodoros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoros.Infrastructure.Context
{
    public class PomodoroContext : IPomodoroContext
    {
        private readonly IOptions<NoSqlDataBaseSettings> _ioptions;
        public IMongoCollection<PomodorosEntity> PomodorosCollection { get; }

        public PomodoroContext(IConfiguration configuration, IOptions<NoSqlDataBaseSettings> ioptions)
        {
            _ioptions = ioptions;
            var client = new MongoClient(_ioptions.Value.ConnectionString);
            var database = client.GetDatabase(_ioptions.Value.DBName);
            PomodorosCollection = database.GetCollection<PomodorosEntity>(_ioptions.Value.CollectionName);

        }
    }
}
