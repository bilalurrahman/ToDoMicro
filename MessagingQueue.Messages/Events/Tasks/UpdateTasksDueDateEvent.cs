using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsBus.Messages.Events.Tasks
{
    public class UpdateTasksDueDateEvent: TasksEntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime DueDate { get; set; }

        public bool HaveReminder { get; set; }
        public DateTime ReminderDateTime { get; set; }
        public bool isNotifiedForReminder { get; set; }
        public bool isPinned { get; set; }
        public bool isNotifiedForDue { get; set; }
        public List<SubTasks> SubTasks { get; set; }


    }
    public class SubTasks: TasksEntityBase
    {
        public string Description { get; set; }

    }

    public class TasksEntityBase : BaseEvent
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool isActive { get; set; } = true;

        public long userId { get; set; }
        public bool isCompleted { get; set; }

        public bool isDeleted { get; set; }

    }
}
