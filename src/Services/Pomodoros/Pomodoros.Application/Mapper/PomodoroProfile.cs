using AutoMapper;
using Pomodoros.Application.Features.Pomodoros.Commad.Get;
using Pomodoros.Application.Features.Pomodoros.Commad.GetAll;
using Pomodoros.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoros.Application.Mapper
{
    public class PomodoroProfile:Profile
    {
        public PomodoroProfile()
        {
            CreateMap<PomodorosEntity, GetAllPomodoroResponse>().ReverseMap();
            CreateMap<PomodorosEntity, GetPomodoroResponse>().ReverseMap();
        }
    }
}
