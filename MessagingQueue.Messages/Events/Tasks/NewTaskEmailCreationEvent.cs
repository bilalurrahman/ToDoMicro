using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsBus.Messages.Events.Tasks
{
    public class NewTaskEmailCreationEvent: BaseEvent
    {
        public userDetails userDetails { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime dueDate { get; set; }
    }
    public class userDetails
    {
        public int userId { get; set; }
        public string email { get; set; } = "bilal.ur.rahman2@gmail.com";// will be replaced.
    }
}
