using GpxMs.GeoService.Application.Models;
using MediatR;

namespace GpxMs.GeoService.Application.Queries
{
    public class ExtendRouteQuery : IRequest<ExtendRouteResponse>
    {
        public ExtendRouteRequest Request { get; private set; }

        public ExtendRouteQuery(ExtendRouteRequest request)
        {
            this.Request = request;
        }
    }
}
