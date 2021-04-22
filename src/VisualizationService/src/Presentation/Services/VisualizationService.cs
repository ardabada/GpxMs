using GpxMs.VisualizationService.Application.Queries;
using GpxMs.VisualizationService.Presentation.Converters;
using GpxMs.VisualizationService.Presentation.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GpxMs.VisualizationService.Presentation.Services
{
    public class VisualizationService : Protos.VisualizationService.VisualizationServiceBase
    {
        private readonly ILogger<VisualizationService> logger;
        private readonly IMediator mediator;
        private readonly IVisualizationServiceConverter converter;

        public VisualizationService(ILogger<VisualizationService> logger,
            IMediator mediator,
            IVisualizationServiceConverter converter)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.converter = converter;
        }

        public override async Task<RouteVisualizationResponseMesage> GetPathImage(RouteVisualizationRequestMessage request, ServerCallContext context)
        {
            logger.LogInformation("Request targeted visualization service");

            var mediatorRequest = converter.ConvertRouteVisualizationRequest(request);
            var response = await mediator.Send(new GetPathImageQuery(mediatorRequest));
            var responseMessage = converter.ConvertRouteVisualizationResponse(response);
            return responseMessage;
        }
    }
}
