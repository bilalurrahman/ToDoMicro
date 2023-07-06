using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsBus.Messages.Events.Tasks
{
    public class DueDateNotificationEvent:BaseEvent
    {
        public userDetails userDetails { get; set; }
        public string title { get; set; }

        public DateTime dueDate { get; set; }
    }
}
