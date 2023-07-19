using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Domain.Entities
{
    public class UserNotificationDevices
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string device_token { get; set; }
    }
}
