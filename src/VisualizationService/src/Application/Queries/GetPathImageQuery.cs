using GpxMs.VisualizationService.Application.Models;
using MediatR;

namespace GpxMs.VisualizationService.Application.Queries
{
    public class GetPathImageQuery : IRequest<RouteVisualizationResponse>
    {
        public RouteVisualizationRequest Request { get; private set; }
        
        public GetPathImageQuery(RouteVisualizationRequest request)
        {
            this.Request = request;
        }
    }
}
