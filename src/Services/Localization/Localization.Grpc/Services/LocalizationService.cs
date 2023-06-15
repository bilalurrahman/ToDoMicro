using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Localization.Application.Features.Localization.Queries.GetResources;
using Localization.Grpc.Protos;
using MediatR;
namespace Localization.Grpc.Services
{
    public class LocalizationService: LocalizationProtoService.LocalizationProtoServiceBase
    {
        private readonly IMediator _mediatR;

        public LocalizationService(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        public override async Task<GetLocalizationResponse> GetLocalization(GetLocalizationRequest request, ServerCallContext context)
        {
            var response = await _mediatR.Send(new GetResourceRequest { languageId = request.LanguageId, resourceKey = request.LocalizationKey });

            return new GetLocalizationResponse
            {
                Text = response.Text
            };
        }
    }
}
