
using Tasks.Domain.Common;

namespace Tasks.Domain.Entities
{
    public class SubTasks : EntityBase
    {
        public string Description { get; set; }
        public bool isCompleted { get; set; }
    }
}
