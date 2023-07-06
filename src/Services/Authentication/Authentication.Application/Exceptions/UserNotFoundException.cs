using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Exceptions
{
    public class UserNotFoundException:ApplicationException
    {
        public UserNotFoundException(string name)
            : base($"Username or password: \"{name}\" not found or incorrect")
        {

        }
    }
}
