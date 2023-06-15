using Localization.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localization.Application.Contracts.Persistance
{
    public interface ILocalizationQueryRepository
    {
        Task<IEnumerable<Records>> GetAll();
    }
}
