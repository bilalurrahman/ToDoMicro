using SharedKernal.Core.Interfaces.AppSettings;
using SharedKernal.Core.Models.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SharedKernal.CacheService
{ 
    public class CachedAppSettingsService : ICachedAppSettingServices
    {
        private readonly IAppSettingsQueryRepository _appSettingsQueryRepository;
        private static AppSettingsModel _allResources;
        private static DateTime _lastRefreshTime;
        public CachedAppSettingsService(IAppSettingsQueryRepository appSettingsQueryRepository)
        {
            _appSettingsQueryRepository = appSettingsQueryRepository;
        }

        public AppSettingsModel appSettingValue(int SettingId)
        {
            if (_lastRefreshTime < DateTime.Now)
            {
                _allResources = _appSettingsQueryRepository.GetAppSettingAsync(SettingId).Result;
                _lastRefreshTime = DateTime.Now.AddMinutes(45);
            }

            return _allResources;
        }
    }
}
