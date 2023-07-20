using MongoDB.Driver;
using Pomodoros.Application.Contracts.Context;
using Pomodoros.Application.Contracts.Persistance.Query;
using Pomodoros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SharedKernal.Common.FaultTolerance;

namespace Pomodoros.Infrastructure.Persistance.Query
{
    public class QueryPomodorosRepository : IQueryPomodorosRepository
    {

        private readonly IPomodoroContext _context;
        private readonly ILogger<QueryPomodorosRepository> logger;
        public QueryPomodorosRepository(IPomodoroContext context, ILogger<QueryPomodorosRepository> logger)
        {
            _context = context;
            this.logger = logger;
        }
        private async Task<T> ExecuteWithFaultPolicy<T>(Func<Task<T>> action)
        {
            return await
                ExecuteWithFaultPolicy(async () => await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(action));
        }
        public async Task<PomodorosEntity> Get(string Id)
        {
            return await
                ExecuteWithFaultPolicy(async () => await _context.PomodorosCollection.Find(p => p.Id == Id).SingleOrDefaultAsync());
        }

        public async Task<List<PomodorosEntity>> GetAll(string TaskId)
        {
            return await
                ExecuteWithFaultPolicy(async () =>
                await _context.PomodorosCollection.Find(p => p.TaskId == TaskId).ToListAsync());
        }
    }
}
