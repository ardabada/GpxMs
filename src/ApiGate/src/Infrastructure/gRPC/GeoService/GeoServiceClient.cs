using GpxMs.ApiGate.Domain.Models;
using GpxMs.ApiGate.Domain.Models.Requests;
using GpxMs.ApiGate.Domain.Models.Responses;
using GpxMs.ApiGate.Infrastructure.gRPC.GeoService.Protos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GpxMs.ApiGate.Infrastructure.gRPC.GeoService
{
    public interface IGeoServiceClient
    {
        Task<ExtendRouteResponse> ExtendRouteAsync(ExtendRouteRequest request, CancellationToken cancellationToken = default);
    }

    public class GeoServiceClient : GrpcClientBase, IGeoServiceClient
    {
        private readonly ILogger<GeoServiceClient> logger;
        private readonly Protos.GeoService.GeoServiceClient client;

        public GeoServiceClient(IGrpcChannelPool grpcChannelPool,
            ILogger<GeoServiceClient> logger,
            IOptionsMonitor<GrpcServicesSettings> settings)
        {
            this.logger = logger;

            client = new Protos.GeoService.GeoServiceClient(grpcChannelPool.GeoServiceChannel);
        }

        public async Task<ExtendRouteResponse> ExtendRouteAsync(ExtendRouteRequest request, CancellationToken cancellationToken = default)
        {
            var result = await client.ExtendRouteAsync(ConvertExtendRouteRequest(request), cancellationToken: cancellationToken);
            return ConvertExtendRouteResponse(result);
        }

        #region Mappings

        private Coord ConvertCoord(CoordMessage message)
        {
            return new Coord { Lat = message.Lat, Long = message.Long };
        }

        private CoordMessage ConvertCoord(Coord model)
        {
            return new CoordMessage()
            {
                Lat = model?.Lat ?? 0,
                Long = model?.Long ?? 0
            };
        }

        private ExtendRouteRequest ConvertExtendRouteRequest(ExtendRouteRequestMessage message)
        {
            return new ExtendRouteRequest()
            {
                Tracks = message.Tracks.Select(x => ConvertTrackExtentionRequest(x)).ToList()
            };
        }

        private ExtendRouteRequestMessage ConvertExtendRouteRequest(ExtendRouteRequest model)
        {
            var result = new ExtendRouteRequestMessage();
            result.Tracks.AddRange(model.Tracks.Select(x => ConvertTrackExtentionRequest(x)));
            return result;
        }

        private ExtendRouteResponse ConvertExtendRouteResponse(ExtendRouteResponseMessage message)
        {
            return new ExtendRouteResponse()
            {
                Tracks = message.Tracks.Select(x => ConvertTrack(x)).ToList()
            };
        }
        private ExtendRouteResponseMessage ConvertExtendRouteResponse(ExtendRouteResponse model)
        {
            var result = new ExtendRouteResponseMessage();
            result.Tracks.AddRange(model.Tracks.Select(x => ConvertTrack(x)));
            return result;
        }

        private Track ConvertTrack(TrackMessage message)
        {
            return new Track { Coords = message.Coords.Select(x => ConvertCoord(x)).ToList() };
        }

        private TrackMessage ConvertTrack(Track model)
        {
            var result = new TrackMessage();
            result.Coords.AddRange(model.Coords.Select(x => ConvertCoord(x)));
            return result;
        }

        private TrackExtentionRequest ConvertTrackExtentionRequest(TrackExtentionRequestMessage message)
        {
            return new TrackExtentionRequest()
            {
                Step = message.Step,
                Track = ConvertTrack(message.Track)
            };
        }

        private TrackExtentionRequestMessage ConvertTrackExtentionRequest(TrackExtentionRequest model)
        {
            return new TrackExtentionRequestMessage()
            {
                Track = ConvertTrack(model.Track),
                Step = model.Step
            };
        }

        private ExtendRouteResponse ConvertTrackResponse(ExtendRouteResponseMessage message)
        {
            return new ExtendRouteResponse()
            {
                Tracks = message.Tracks.Select(x => ConvertTrack(x)).ToList()
            };
        }

        private ExtendRouteResponseMessage ConvertTrackResponse(ExtendRouteResponse model)
        {
            var result = new ExtendRouteResponseMessage();
            result.Tracks.AddRange(model.Tracks.Select(x => ConvertTrack(x)));
            return result;
        }
    }

    #endregion
}
