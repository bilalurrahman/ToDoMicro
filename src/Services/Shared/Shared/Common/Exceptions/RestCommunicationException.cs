﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Common.Exceptions
{
    [Serializable]
    public class RestCommunicationException : Exception
    {
        public EventId ErrorEvent { get; private set; }
        private const string RestCommunicationErrorsKey = "RestCommunicationErrors";

        public RestCommunicationException(string message) : base(message)
        {
        }

        public Dictionary<int, string> Errors
        {
            get
            {
                return Data[RestCommunicationErrorsKey] as Dictionary<int, string>;
            }
        }
        public RestCommunicationException(int errorcode, string message) : base(message)
        {
            ErrorEvent = new EventId(errorcode, message);
        }
    }
}