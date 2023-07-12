using Microsoft.Extensions.Logging;
using SharedKernal.Common.Exceptions.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Common.Exceptions
{
    public class LogEventIds
    {
        public class BusinessRuleEventIds
        {
            public static EventId UserNotAvailable => new EventId(100001, BusinessRuleExceptionResources.UserNotAvailable);
            public static EventId TitleCantBeEmpty => new EventId(100002, BusinessRuleExceptionResources.TasksTitleCantbeEmpty);

        }
        public class EntityNotFoundEventIds
        {
            public static EventId IncorrectUserName => new EventId(200001, EntityNotFoundExceptionResources.IncorrectUserName);
            public static EventId TaskIdNotFound => new EventId(200002, EntityNotFoundExceptionResources.TaskIdNotFound);

        }

        public class CommonEventIds
        {
            public static EventId InternalServerError => new EventId(300001, CommonExceptionResources.InternalServerError);
        }


        public class ValidationEventIds
        {
            public static EventId NotEmptyOrOthers => new EventId(400001, ValidationExceptionResources.UserShouldNotBeEmpty);

        }
    }
}
