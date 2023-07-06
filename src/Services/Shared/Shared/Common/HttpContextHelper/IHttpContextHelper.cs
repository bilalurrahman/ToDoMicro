using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Common.HttpContextHelper
{
    public interface IHttpContextHelper
    {
        string CurrentLoggedInId { get; }
        string CurrentLocalization { get; }
    }
}
