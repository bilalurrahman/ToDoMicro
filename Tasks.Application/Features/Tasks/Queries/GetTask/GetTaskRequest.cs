using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Tasks.Application.Features.Tasks.Queries.GetTask
{
    public class GetTaskRequest:IRequest<GetTaskResponse>
    {
        public string Id { get; set; }
    }
}
