using MongoDB.Driver;
using Pomodoros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoros.Application.Contracts.Context
{
    public interface IPomodoroContext
    {
        IMongoCollection<PomodorosEntity> PomodorosCollection { get; }
    }
}
