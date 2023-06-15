using Localization.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localization.Domain.Entities
{
    public class Records:EntityBase
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Text { get; set; }
        public int LanguageId { get; set; } 
        public virtual LanguagesEntity LocalizationLanguage { get; set; }
   

        public Records Update(string key, string text, int langId)
        {
            Key = key;
            Text = text;
            LanguageId = langId;
            return this;
        }
    }
}
