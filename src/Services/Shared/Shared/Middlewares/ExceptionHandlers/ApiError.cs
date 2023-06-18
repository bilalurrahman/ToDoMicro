using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Middlewares.ExceptionHandlers
{
    public class ApiError
    {
        public Guid Id { get; set; }
        public string ErrorMessage { get; set; }
        public int Status { get; set; }
        public int ErrorCode { get; set; }
        public string StackTrace { get; set; }

        public ApiError()
        {
            Id = Guid.NewGuid();
        }


    }
}
