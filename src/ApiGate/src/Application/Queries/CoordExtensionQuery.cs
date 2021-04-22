using GpxMs.ApiGate.Domain.Models.Requests;
using GpxMs.ApiGate.Domain.Models.Responses;
using MediatR;

namespace GpxMs.ApiGate.Application.Queries
{
    public class CoordExtensionQuery : IRequest<ExtendRouteResponse>
    {
        public ExtendRouteRequest Request { get; }

        public CoordExtensionQuery(ExtendRouteRequest request)
        {
            this.Request = request;
        }
    }
}
