using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Pomodoros.Application.Features.Pomodoros.Commad.Update
{

    public class UpdatePomodoroRequest:IRequest<UpdatePomodoroResponse>
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
    }
}
