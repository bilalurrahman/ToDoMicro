using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Common.Exceptions.Resources
{
    public class RestCommunicationExceptionResources : Resources
    {
        public static string CommonCommunicationError => GetResources("CommonCommunicationError");
        public static string RetryAttemptedError => GetResources("RetryAttemptedError");
        public static string BadRequestError => GetResources("BadRequestError");


    }
}
