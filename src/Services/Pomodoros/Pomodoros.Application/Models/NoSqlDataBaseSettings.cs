using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoros.Application.Models
{
    public class NoSqlDataBaseSettings
    {
        public string ConnectionString { get; set; }
        public string DBName { get; set; }
        public string CollectionName { get; set; }
    }
}
