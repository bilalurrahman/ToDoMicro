using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Entities;

namespace Tasks.Application.Features.Tasks.Commands.InsertTasks
{
    public class InsertTasksRequest:IRequest<InsertTasksResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        public DateTime DueDate { get; set; }

        public bool HaveReminder { get; set; }

        public bool isPinned { get; set; }

        public SubTasks SubTasks { get; set; }
    }
}
