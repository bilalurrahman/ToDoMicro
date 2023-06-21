using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Common;

namespace Tasks.Application.Features.Tasks.Queries
{
    public class GetAllTasksRequest: IRequest<List<GetAllTasksResponse>>
    {
        public long UserId { get; set; }
    }
}
