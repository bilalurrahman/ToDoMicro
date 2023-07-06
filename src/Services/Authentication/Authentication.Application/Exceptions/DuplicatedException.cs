using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Exceptions
{
    public class UserDuplicatedException:ApplicationException
    {
        public UserDuplicatedException()
            : base($"Username is not available")
        {

        }
    }
}
