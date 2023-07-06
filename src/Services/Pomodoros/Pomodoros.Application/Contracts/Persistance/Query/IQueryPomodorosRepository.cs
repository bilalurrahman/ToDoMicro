using Pomodoros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoros.Application.Contracts.Persistance.Query
{
    public interface IQueryPomodorosRepository
    {
        Task<PomodorosEntity> Get(string Id);
        Task<List<PomodorosEntity>> GetAll(string TaskId);
    }
}
