using MongoDB.Driver;
using Pomodoros.Application.Contracts.Context;
using Pomodoros.Application.Contracts.Persistance.Command;
using Pomodoros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SharedKernal.Common.FaultTolerance;

namespace Pomodoros.Infrastructure.Persistance.Command
{
    public class CommandPomodorosRepository : ICommandPomodorosRepository
    {
        private readonly IPomodoroContext _context;
        private readonly ILogger<CommandPomodorosRepository> logger;
        public CommandPomodorosRepository(IPomodoroContext context, ILogger<CommandPomodorosRepository> logger)
        {
            _context = context;
            this.logger = logger;
        }

        private async Task<T> ExecuteWithFaultPolicy<T>(Func<Task<T>> action)
        {
            return await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(action);
        }
        public async Task<string> Add(PomodorosEntity entity)
        {
            await Resiliance.serviceFaultPolicy(logger).Result.ExecuteAsync(async () =>
            await _context.PomodorosCollection.InsertOneAsync(entity));
            return entity.Id;
        }

        public async Task<bool> Update(PomodorosEntity entity)
        {
            var updateResult = await
                ExecuteWithFaultPolicy(async () => await _context
                .PomodorosCollection
                .ReplaceOneAsync(filter: g => g.Id == entity.Id, replacement: entity));

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }
    }
}
