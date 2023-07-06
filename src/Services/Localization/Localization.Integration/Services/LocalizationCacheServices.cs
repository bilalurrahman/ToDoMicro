using Localization.Application.Contracts.Persistance;
using Localization.Application.Contracts.Services;
using Localization.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
namespace Localization.Integration.Services
{
    public class LocalizationCacheService : ILocalizationCacheServices
    {
        private static List<Records> _allResources;
        private readonly ILocalizationQueryRepository _repository;        
        private static DateTime _lastRefreshTime;

        public LocalizationCacheService(ILocalizationQueryRepository repository)
        {
            _repository = repository;          
            loadResources();
            Timer timer = new Timer(3600000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        public List<Records> AllResources
        {
            get
            {
                return _allResources;
            }
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            loadResources();
        }
        private void loadResources()
        {
            if (_lastRefreshTime < DateTime.Now)
            {
                _allResources = _repository.GetAll().Result.ToList();
                _lastRefreshTime = DateTime.Now.AddMinutes(30);
            }
        }        
    }
}
