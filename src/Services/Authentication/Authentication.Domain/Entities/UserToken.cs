using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Domain.Entities
{
    public class UserToken:RegisterUser
    {
        public string refresh_token { get; set; }
        public DateTime refresh_token_expiry { get; set; }
    }
}
