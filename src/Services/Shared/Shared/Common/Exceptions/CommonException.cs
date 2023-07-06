using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Common.Exceptions
{
    [Serializable]
    public class CommonException : Exception
    {
        public EventId ErrorEvent { get; private set; }
        private const string CommonErrorsKey = "CommonErrors";

        public CommonException(string message) : base(message)
        {
        }

        public Dictionary<int, string> Errors
        {
            get
            {
                return Data[CommonErrorsKey] as Dictionary<int, string>;
            }
        }
        public CommonException(int errorcode, string message) : base(message)
        {
            ErrorEvent = new EventId(errorcode, message);
        }
    }
}
