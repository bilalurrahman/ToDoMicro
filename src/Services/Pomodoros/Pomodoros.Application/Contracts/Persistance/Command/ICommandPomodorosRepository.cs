using Pomodoros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoros.Application.Contracts.Persistance.Command
{
    public interface ICommandPomodorosRepository
    {
        Task<string> Add(PomodorosEntity entity);
        Task<bool> Update(PomodorosEntity entity);
    }
}
