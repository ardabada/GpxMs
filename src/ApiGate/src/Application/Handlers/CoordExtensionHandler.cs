using GpxMs.ApiGate.Application.Queries;
using GpxMs.ApiGate.Domain.Models.Responses;
using GpxMs.ApiGate.Infrastructure.gRPC.GeoService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GpxMs.ApiGate.Application.Handlers
{
    public class CoordExtensionHandler : IRequestHandler<CoordExtensionQuery, ExtendRouteResponse>
    {
        private readonly IGeoServiceClient geoServiceClient;

        public CoordExtensionHandler(IGeoServiceClient geoServiceClient)
        {
            this.geoServiceClient = geoServiceClient;
        }

        public async Task<ExtendRouteResponse> Handle(CoordExtensionQuery request, CancellationToken cancellationToken)
        {
            return await geoServiceClient.ExtendRouteAsync(request.Request, cancellationToken);
        }
    }
}
