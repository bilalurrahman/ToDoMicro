using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Common.Exceptions
{
    [Serializable]
    public class BusinessRuleException : Exception
    {
        public EventId ErrorEvent { get; private set; }
        private const string BusinessErrorsKey = "BusinessErrors";
        public BusinessRuleException(string message) : base(message)
        {
        }

        public Dictionary<int,string> Errors
        {
            get
            {
                return Data[BusinessErrorsKey] as Dictionary<int, string>;
            }
        }
        public BusinessRuleException(int errorcode,string message) : base(message)
        {
            ErrorEvent = new EventId(errorcode,message);
        }

    }
}
