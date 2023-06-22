using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Common;
using Tasks.Domain.Entities;

namespace Tasks.Application.Features.Tasks.Commands.InsertTasks
{
    public class InsertTasksRequest: TasksEntity, IRequest<InsertTasksResponse>
    {
    }
}
