using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcement.Application.Models
{
    public class UserDevicesModel
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string device_token { get; set; }

    }
}
