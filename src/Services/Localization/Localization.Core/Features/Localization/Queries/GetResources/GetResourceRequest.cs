using MediatR;

namespace Localization.Application.Features.Localization.Queries.GetResources
{
    public class GetResourceRequest : IRequest<GetResourceResponse>
    {
        public string resourceKey { get; set; }
        public int languageId { get; set; }
    }
}
