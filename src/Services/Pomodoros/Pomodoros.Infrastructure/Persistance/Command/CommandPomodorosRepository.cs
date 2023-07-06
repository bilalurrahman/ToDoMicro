using MongoDB.Driver;
using Pomodoros.Application.Contracts.Context;
using Pomodoros.Application.Contracts.Persistance.Command;
using Pomodoros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoros.Infrastructure.Persistance.Command
{
    public class CommandPomodorosRepository : ICommandPomodorosRepository
    {
        private readonly IPomodoroContext _context;

        public CommandPomodorosRepository(IPomodoroContext context)
        {
            _context = context;
        }

        public async Task<string> Add(PomodorosEntity entity)
        {
            await _context.PomodorosCollection.InsertOneAsync(entity);
            return entity.Id;
        }

        public async Task<bool> Update(PomodorosEntity entity)
        {
            var updateResult = await _context
                .PomodorosCollection
                .ReplaceOneAsync(filter: g => g.Id == entity.Id, replacement: entity);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }
    }
}
