using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Core.Models.AppSettings
{
   public  class AppSettingsModel
    {
        public int Id { get; set; }
        public string AppSettingName { get; set; }
        public string AppSettingValue { get; set; }

    }
}
