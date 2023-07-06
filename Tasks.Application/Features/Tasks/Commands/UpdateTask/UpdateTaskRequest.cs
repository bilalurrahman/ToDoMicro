using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tasks.Domain.Entities;

namespace Tasks.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskRequest:TasksEntity,IRequest<UpdateTaskResponse>
    {
        
    }
}
