using GpxMs.GeoService.Application.Models;
using GpxMs.GeoService.Application.Queries;
using GpxMs.GeoService.Presentation.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace GpxMs.GeoService.Presentation.Services
{
    public class GeoService : Protos.GeoService.GeoServiceBase
    {
        private readonly ILogger<GeoService> logger;
        private readonly IMediator mediator;

        public GeoService(ILogger<GeoService> logger,
            IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        public override async Task<ExtendRouteResponseMessage> ExtendRoute(ExtendRouteRequestMessage request, ServerCallContext context)
        {
            var mediatorRequest = new ExtendRouteQuery(request.Tracks.Select(x => convertTrack(x)).ToList(), request.Step);
            var mediatorResponse = await mediator.Send(mediatorRequest);

            var result = new ExtendRouteResponseMessage();
            result.Tracks.AddRange(mediatorResponse.Select(x => convertTrack(x)));
            return result;
        }

        public override async Task<ProcessTimeResponseMessage> ProcessTime(ProcessTimeRequestMessage request, ServerCallContext context)
        {
            var mediatorRequest = new ProcessTimeQuery(request.Tracks.Select(x => convertTrack(x)).ToList(), request.StartTime.ToDateTime(), request.TrackSplits.Select(x => x.ToDateTime()).ToList());
            var mediatorResponse = await mediator.Send(mediatorRequest);

            var result = new ProcessTimeResponseMessage();
            result.Tracks.AddRange(mediatorResponse.Select(x => convertTimedTrack(x)));
            return result;
        }


        private CoordMessage convertCoord(Coord coord)
        {
            return new CoordMessage() { Lat = coord.Lat, Long = coord.Long };
        }
        private Coord convertCoord(CoordMessage message)
        {
            return new Coord(message.Lat, message.Long);
        }
        private TimedCoordMessage convertTimedCoord(TimedCoord coord)
        {
            return new TimedCoordMessage()
            {
                Lat = coord.Lat,
                Long = coord.Long,
                Time = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(coord.Time)
            };
        }
        private Track convertTrack(TrackMessage message)
        {
            return new Track(message.Coords.Select(x => convertCoord(x)).ToList());
        }
        private TrackMessage convertTrack(Track track)
        {
            var message = new TrackMessage();
            message.Coords.AddRange(track.Select(x => convertCoord(x)));
            return message;
        }
        private TimedTrackMessage convertTimedTrack(TimedTrack track)
        {
            var message = new TimedTrackMessage();
            message.Coords.AddRange(track.ConvertAll(x => convertTimedCoord(x)));
            return message;
        } 
    }
}
