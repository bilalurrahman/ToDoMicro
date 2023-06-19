using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Common.Exceptions.Resources
{
    class EntityNotFoundExceptionResources:Resources
    {
        public static string IncorrectUserName => GetResources("UserNotFound");
    }
}
