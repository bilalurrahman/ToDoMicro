using Localization.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localization.Application.Contracts.Services
{
    public interface ILocalizationCacheServices
    {
        List<Records> AllResources { get; }
    }
}
