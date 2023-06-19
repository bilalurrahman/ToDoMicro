using Localization.Grpc.Protos;
using SharedKernal.GrpcServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Common.Exceptions.Resources
{
    public class Resources
    {
        public static string GetResources(string key)
        {

            LocalizationGrpcServices _localizationService = (LocalizationGrpcServices)ContainerManager.Container.GetService(typeof(LocalizationGrpcServices));
            //LocalizationGrpcServices localizationGrpcServices;

            return _localizationService.GetLocalizationResponse(key).Result;
                        
            //Call Grpc from here.
        }
    }
}
