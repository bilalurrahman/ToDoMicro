using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Common.Exceptions.Resources
{
    public class ValidationExceptionResources:Resources
    {
        public static string UserShouldNotBeEmpty => GetResources("UserShouldNotBeEmpty");
        public static string PasswordShouldNotBeEmpty => GetResources("PasswordShouldNotBeEmpty");


    }
}
