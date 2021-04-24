using GpxMs.ApiGate.Application.Commands;
using GpxMs.ApiGate.Domain.Models.Responses;
using GpxMs.ApiGate.Infrastructure.gRPC.GeoService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GpxMs.ApiGate.Application.Handlers
{
    public class BuildAndAnalyzeHandler : IRequestHandler<BuildAndAnalyzeCommand, BuildAndAnalyzeResponse>
    {
        private readonly IGeoServiceClient geoServiceClient;

        public BuildAndAnalyzeHandler(IGeoServiceClient geoServiceClient)
        {
            this.geoServiceClient = geoServiceClient;
        }

        public async Task<BuildAndAnalyzeResponse> Handle(BuildAndAnalyzeCommand request, CancellationToken cancellationToken)
        {
            var extendedTracks = await geoServiceClient.ExtendRouteAsync(request.Request.Tracks, 0.0001, cancellationToken);
            var timedTracks = await geoServiceClient.ProcessTime(extendedTracks, request.Request.Start, request.Request.Splits, cancellationToken);
            var savedId = await geoServiceClient.SaveTimedTrack(timedTracks, cancellationToken);
            return new BuildAndAnalyzeResponse() { Id = savedId };
        }
    }
}
