
using SharedKernal.GrpcServices;

namespace SharedKernal.Common.Exceptions.Resources
{
    public class Resources
    {
        public static string GetResources(string key)
        {

            LocalizationGrpcServices _localizationService = (LocalizationGrpcServices)ContainerManager.Container.GetService(typeof(LocalizationGrpcServices));
            return _localizationService.GetLocalizationResponse(key).Result;

        }
    }
}
