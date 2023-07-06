using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Tasks.Application.Features.Tasks.Commands.DeleteTask
{
    public class DeleteTasksRequest:IRequest<DeleteTaskResponse>
    {
        public string Id { get; set; }
    }
}
