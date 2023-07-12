using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Common.Exceptions
{
    [Serializable]
    public class ValidationsException : Exception
    {
        public EventId ErrorEvent { get; private set; }
        private const string ValidationErrorsKey = "ValidationErrors";
        public ValidationsException(string message) : base(message)
        {
        }

        public Dictionary<int,string> Errors
        {
            get
            {
                return Data[ValidationErrorsKey] as Dictionary<int, string>;
            }
        }
        public ValidationsException(int errorcode,string message) : base(message)
        {
            ErrorEvent = new EventId(errorcode,message);
        }

    }
}
