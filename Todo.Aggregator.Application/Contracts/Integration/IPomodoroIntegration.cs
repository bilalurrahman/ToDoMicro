﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Aggregator.Application.Models;

namespace Todo.Aggregator.Application.Contracts.Integration
{
    public interface IPomodoroIntegration
    {
        Task<List<Pomodoros>> GetPomodoros(string TaskId);
    }
}