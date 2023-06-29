using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Pomodoros.Application.Features.Pomodoros.Commad.GetAll
{
    public class GetAllPomodoroRequest:IRequest<List<GetAllPomodoroResponse>>
    {
        public string TaskId { get; set; }
    }
}
