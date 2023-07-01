﻿using SharedKernal.Core.Models.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Core.Interfaces.AppSettings
{
    public interface IAppSettingsQueryRepository
    {
        Task<AppSettingsModel> GetAppSettingAsync(int appSettingId);
    }
}
