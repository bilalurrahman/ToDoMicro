using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsBus.Messages.Events
{
    public class BaseEvent
    {
        public Guid EventId { get; private set; }
        public DateTime EventCreationDate { get; set; }

        public BaseEvent()
        {
            EventId = Guid.NewGuid();
            EventCreationDate = DateTime.UtcNow;
        }
        public BaseEvent(Guid id, DateTime creationDate)
        {
            EventId = id;
            EventCreationDate = creationDate;
        }
    }
}
