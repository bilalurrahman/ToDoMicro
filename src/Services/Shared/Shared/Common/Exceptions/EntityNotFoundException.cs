using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Common.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EventId ErrorEvent { get; private set; }
        private const string EntityErrorsKey = "EntityNotFoundErrors";
        public EntityNotFoundException(string message) : base(message)
        {

        }
        public Dictionary<int, string> Errors
        {
            get
            {
                return Data[EntityErrorsKey] as Dictionary<int, string>;
            }
        }

        public EntityNotFoundException(int errorCode,string message) : base(message)
        {
            ErrorEvent = new EventId(errorCode, message);
        }
    }
}
