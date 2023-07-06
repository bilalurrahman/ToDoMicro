using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Pomodoros.Application.Features.Pomodoros.Commad.Get
{
    public class GetPomodoroRequest:IRequest<GetPomodoroResponse>
    {
        public string Id { get; set; }
    }
}
