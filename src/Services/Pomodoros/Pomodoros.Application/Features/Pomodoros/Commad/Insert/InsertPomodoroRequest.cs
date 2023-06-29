using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Pomodoros.Application.Features.Pomodoros.Commad.Insert
{
    public class InsertPomodoroRequest:IRequest<InsertPomodoroResponse>
    {
        public string TaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
    }
}
