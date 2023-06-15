using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Localization.Application.Contracts.Services;
using MediatR;
namespace Localization.Application.Features.Localization.Queries.GetResources
{
    public class GetResourcesHandler : IRequestHandler<GetResourceRequest, GetResourceResponse>
    {
        private readonly ILocalizationCacheServices _localizationCacheServices;

        public GetResourcesHandler(ILocalizationCacheServices localizationCacheServices)
        {
            _localizationCacheServices = localizationCacheServices;
        }

        public async Task<GetResourceResponse> Handle(GetResourceRequest request, CancellationToken cancellationToken)
        {
            string result = string.Empty;

            if (request.resourceKey == null)
            {
                request.resourceKey = string.Empty;
            }
            var localeResource = _localizationCacheServices.AllResources.FirstOrDefault(
                x => string.Equals(x.Key, request.resourceKey, StringComparison.OrdinalIgnoreCase)
                && x.LanguageId == request.languageId);

            result = localeResource == null ? request.resourceKey : localeResource.Text;

            return new GetResourceResponse
            {
                Text = result
            };

        }
    }
}
