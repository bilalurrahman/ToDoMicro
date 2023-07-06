using Authentication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Features.Register.Queries.GetUser
{
    public class GetUserResponse:EntityBase
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
