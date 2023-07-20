using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Common.Exceptions
{
   public class CustomValidationModel
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
    }

    public class CustomValidation: Resources.Resources
    {
        public CustomValidation(List<CustomValidationModel> valdations)
        {
            
            string Resource = "";
            if (valdations != null)
            {
                foreach(var validation in valdations)
                {                   
                    Resource = $"{validation.ErrorCode}_{validation.PropertyName}";
                    break;
                }
            }
            int logId = LogEventIds.ValidationEventIds.NotEmptyOrOthers.Id;          
            throw new ValidationsException(logId, GetResources(Resource.ToUpper()));
        }
    }
}
