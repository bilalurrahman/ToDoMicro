using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsBus.Messages.Events.Tasks
{
    public class UpdateTasksReminderDateEvent : TasksEntityBase
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
        public bool? IsRepeat { get; set; } = false;
        public int? RepeatFrequency { get; set; }
        public DateTime? NextDueDateForRepeat { get; set; }


    }
}
