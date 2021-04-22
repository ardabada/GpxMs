using GpxMs.GeoService.Application.Queries;
using GpxMs.GeoService.Presentation.Converters;
using GpxMs.GeoService.Presentation.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GpxMs.GeoService.Presentation.Services
{
    public class GeoService : Protos.GeoService.GeoServiceBase
    {
        private readonly ILogger<GeoService> logger;
        private readonly IMediator mediator;
        private readonly IGeoServiceConverter converter;

        public GeoService(ILogger<GeoService> logger,
            IMediator mediator,
            IGeoServiceConverter converter)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.converter = converter;
        }

        public override async Task<ExtendRouteResponseMessage> ExtendRoute(ExtendRouteRequestMessage request, ServerCallContext context)
        {
            logger.LogInformation("Extending route for @count tracks", request.Tracks.Count);

            var mediatorRequest = converter.ConvertExtendRouteRequest(request);
            var response = await mediator.Send(new ExtendRouteQuery(mediatorRequest));
            var responseMessage = converter.ConvertExtendRouteResponse(response);

            logger.LogInformation("Tracks extended", request.Tracks.Count);
            return responseMessage;
        }
    }
}
