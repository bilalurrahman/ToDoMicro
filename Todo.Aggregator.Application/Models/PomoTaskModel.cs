using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Aggregator.Application.Models
{
    public class PomoTaskModel: TaskBase
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
        public List<Pomodoros> Pomodoros { get; set; } 
    }
    public class Pomodoros : PomoBase
    {
        public string TaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
    }
    
    public class PomoBase
    {
     public string Id { get; set; }
    }

    public class TaskBase
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
    public class SubTasks : TaskBase
    {
        public string Description { get; set; }

    }

}
