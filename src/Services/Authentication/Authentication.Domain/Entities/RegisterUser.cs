using Authentication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Domain.Entities
{
    public class RegisterUser : EntityBase
    {       
        public string Username { get; set; }
        public string Password{ get; set; }
    }
}
