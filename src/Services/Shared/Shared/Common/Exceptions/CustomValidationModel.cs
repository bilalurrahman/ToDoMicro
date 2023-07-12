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

    public class CustomValidation
    {
        public CustomValidation(List<CustomValidationModel> valdations)
        {
            string allValidations = "";
            if (valdations != null)
            {
                foreach(var validation in valdations)
                {
                    allValidations += $"{validation.ErrorCode}:" +
                        validation.ErrorMessage + "  \n\r";
                }
            }
            throw new ValidationsException(LogEventIds.ValidationEventIds.NotEmptyOrOthers.Id, allValidations);
        }
    }
}
