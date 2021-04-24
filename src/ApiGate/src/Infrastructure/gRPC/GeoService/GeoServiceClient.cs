using Google.Protobuf.WellKnownTypes;
using GpxMs.ApiGate.Domain.Models;
using GpxMs.ApiGate.Infrastructure.gRPC.GeoService.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GpxMs.ApiGate.Infrastructure.gRPC.GeoService
{
    public interface IGeoServiceClient
    {
        Task<List<Track>> ExtendRouteAsync(List<Track> tracks, double step, CancellationToken cancellationToken = default);
        Task<List<TimedTrack>> ProcessTime(List<Track> tracks, DateTime start, List<DateTime> splits, CancellationToken cancellationToken = default);
        Task<string> SaveTimedTrack(List<TimedTrack> tracks, CancellationToken cancellationToken = default);
    }

    public class GeoServiceClient : GrpcClientBase, IGeoServiceClient
    {
        private readonly ILogger<GeoServiceClient> logger;
        private readonly Protos.GeoService.GeoServiceClient client;

        public GeoServiceClient(IGrpcChannelPool grpcChannelPool,
            ILogger<GeoServiceClient> logger)
        {
            this.logger = logger;

            client = new Protos.GeoService.GeoServiceClient(grpcChannelPool.GeoServiceChannel);
        }

        public async Task<List<Track>> ExtendRouteAsync(List<Track> tracks, double step, CancellationToken cancellationToken = default)
        {
            var message = new ExtendRouteRequestMessage();
            message.Step = step;
            message.Tracks.AddRange(tracks.Select(x => convertTrack(x)));
            var result = await client.ExtendRouteAsync(message, cancellationToken: cancellationToken);
            return result.Tracks.Select(x => convertTrack(x)).ToList();
        }

        public async Task<List<TimedTrack>> ProcessTime(List<Track> tracks, DateTime start, List<DateTime> splits, CancellationToken cancellationToken = default)
        {
            var message = new ProcessTimeRequestMessage();
            message.StartTime = Timestamp.FromDateTime(start);
            message.TrackSplits.AddRange(splits.Select(x => Timestamp.FromDateTime(x)));
            message.Tracks.AddRange(tracks.Select(x => convertTrack(x)));
            var result = await client.ProcessTimeAsync(message, cancellationToken: cancellationToken);
            return result.Tracks.Select(x => convertTrack(x)).ToList();
        }

        public async Task<string> SaveTimedTrack(List<TimedTrack> tracks, CancellationToken cancellationToken = default)
        {
            var message = new SaveRequestMessage();
            message.Tracks.AddRange(tracks.Select(x => convertTrack(x)));
            var result = await client.SaveTimedTrackAsync(message, cancellationToken: cancellationToken);
            return result.Id;
        }

        private CoordMessage convertCoord(Coord coord)
        {
            return new CoordMessage() { Lat = coord.Lat, Long = coord.Long };
        }
        private Coord convertCoord(CoordMessage message)
        {
            return new Coord(message.Lat, message.Long);
        }
        private TimedCoord convertCoord(TimedCoordMessage message)
        {
            return new TimedCoord(message.Lat, message.Long, message.Time.ToDateTime());
        }
        private TimedCoordMessage convertCoord(TimedCoord message)
        {
            return new TimedCoordMessage()
            {
                Lat = message.Lat,
                Long = message.Long,
                Time = Timestamp.FromDateTime(message.Time)
            };
        }
        private TrackMessage convertTrack(Track track)
        {
            var message = new TrackMessage();
            message.Coords.AddRange(track.Coords.Select(x => convertCoord(x)));
            return message;
        }
        private Track convertTrack(TrackMessage message)
        {
            return new Track() { Coords = message.Coords.Select(x => convertCoord(x)).ToList() };
        }
        private TimedTrack convertTrack(TimedTrackMessage message)
        {
            return new TimedTrack() { Coords = message.Coords.Select(x => convertCoord(x)).ToList() };
        }
        private TimedTrackMessage convertTrack(TimedTrack track)
        {
            var message = new TimedTrackMessage();
            message.Coords.AddRange(track.Coords.Select(x => convertCoord(x)));
            return message;
        }
    }
}