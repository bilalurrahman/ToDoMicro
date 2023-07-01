using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Common.Exceptions.Resources
{
    public class BusinessRuleExceptionResources:Resources
    {
        public static string UserNotAvailable => GetResources("UserDuplicated");
        public static string TasksTitleCantbeEmpty => GetResources("TasksTitleCantbeEmpty");


    }
}
