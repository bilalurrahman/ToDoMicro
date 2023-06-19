using Localization.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.GrpcServices
{
    public class LocalizationGrpcServices
    {
        private readonly LocalizationProtoService.LocalizationProtoServiceClient _localizationProtoServiceClient;

        public LocalizationGrpcServices(LocalizationProtoService.LocalizationProtoServiceClient localizationProtoServiceClient)
        {
            _localizationProtoServiceClient = localizationProtoServiceClient;
        }

        public async Task<string> GetLocalizationResponse(string key, int languageId=2)
        {
            var request = new GetLocalizationRequest{
                LanguageId = languageId,
                LocalizationKey = key
            };

            var resp =  await _localizationProtoServiceClient.GetLocalizationAsync(request);

            return resp?.Text;
        }
    }
}
