using MongoDB.Driver;
using Pomodoros.Application.Contracts.Context;
using Pomodoros.Application.Contracts.Persistance.Query;
using Pomodoros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoros.Infrastructure.Persistance.Query
{
    public class QueryPomodorosRepository : IQueryPomodorosRepository
    {

        private readonly IPomodoroContext _context;
        public QueryPomodorosRepository(IPomodoroContext context)
        {
            _context = context;
        }
        public async Task<PomodorosEntity> Get(string Id)
        {
            return await _context.PomodorosCollection.Find(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<PomodorosEntity>> GetAll(string TaskId)
        {
            return await _context.PomodorosCollection.Find(p => p.TaskId == TaskId).ToListAsync();
        }
    }
}
