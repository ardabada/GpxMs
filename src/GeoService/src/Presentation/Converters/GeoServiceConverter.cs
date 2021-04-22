using GpxMs.GeoService.Application.Models;
using GpxMs.GeoService.Presentation.Protos;
using System;
using System.Linq;

namespace GpxMs.GeoService.Presentation.Converters
{
    public interface IGeoServiceConverter
    {
        Coord ConvertCoord(CoordMessage message);
        CoordMessage ConvertCoord(Coord model);

        Track ConvertTrack(TrackMessage message);
        TrackMessage ConvertTrack(Track model);

        ExtendRouteRequest ConvertExtendRouteRequest(ExtendRouteRequestMessage message);
        ExtendRouteRequestMessage ConvertExtendRouteRequest(ExtendRouteRequest model);

        ExtendRouteResponse ConvertExtendRouteResponse(ExtendRouteResponseMessage message);
        ExtendRouteResponseMessage ConvertExtendRouteResponse(ExtendRouteResponse model);

        TrackExtentionRequest ConvertTrackExtentionRequest(TrackExtentionRequestMessage message);
        TrackExtentionRequestMessage ConvertTrackExtentionRequest(TrackExtentionRequest model);

        ExtendRouteResponse ConvertTrackResponse(ExtendRouteResponseMessage message);
        ExtendRouteResponseMessage ConvertTrackResponse(ExtendRouteResponse model);
    }

    public class GeoServiceConverter : IGeoServiceConverter
    {
        public Coord ConvertCoord(CoordMessage message)
        {
            return new Coord(message.Lat, message.Long);
        }

        public CoordMessage ConvertCoord(Coord model)
        {
            return new CoordMessage()
            {
                Lat = model?.Lat ?? 0,
                Long = model?.Long ?? 0
            };
        }

        public ExtendRouteRequest ConvertExtendRouteRequest(ExtendRouteRequestMessage message)
        {
            return new ExtendRouteRequest()
            {
                Tracks = message.Tracks.Select(x => ConvertTrackExtentionRequest(x)).ToList()
            };
        }

        public ExtendRouteRequestMessage ConvertExtendRouteRequest(ExtendRouteRequest model)
        {
            var result = new ExtendRouteRequestMessage();
            result.Tracks.AddRange(model.Tracks.Select(x => ConvertTrackExtentionRequest(x)));
            return result;
        }

        public ExtendRouteResponse ConvertExtendRouteResponse(ExtendRouteResponseMessage message)
        {
            return new ExtendRouteResponse()
            {
                Tracks = message.Tracks.Select(x => ConvertTrack(x)).ToList()
            };
        }
        public ExtendRouteResponseMessage ConvertExtendRouteResponse(ExtendRouteResponse model)
        {
            var result = new ExtendRouteResponseMessage();
            result.Tracks.AddRange(model.Tracks.Select(x => ConvertTrack(x)));
            return result;
        }

        public Track ConvertTrack(TrackMessage message)
        {
            return new Track(message.Coords.Select(x => ConvertCoord(x)).ToList());
        }

        public TrackMessage ConvertTrack(Track model)
        {
            var result = new TrackMessage();
            result.Coords.AddRange(model.Coords.Select(x => ConvertCoord(x)));
            return result;
        }

        public TrackExtentionRequest ConvertTrackExtentionRequest(TrackExtentionRequestMessage message)
        {
            return new TrackExtentionRequest()
            {
                Step = message.Step,
                Track = ConvertTrack(message.Track)
            };
        }

        public TrackExtentionRequestMessage ConvertTrackExtentionRequest(TrackExtentionRequest model)
        {
            return new TrackExtentionRequestMessage()
            {
                Track = ConvertTrack(model.Track),
                Step = model.Step
            };
        }

        public ExtendRouteResponse ConvertTrackResponse(ExtendRouteResponseMessage message)
        {
            return new ExtendRouteResponse()
            {
                Tracks = message.Tracks.Select(x => ConvertTrack(x)).ToList()
            };
        }

        public ExtendRouteResponseMessage ConvertTrackResponse(ExtendRouteResponse model)
        {
            var result = new ExtendRouteResponseMessage();
            result.Tracks.AddRange(model.Tracks.Select(x => ConvertTrack(x)));
            return result;
        }
    }
}
