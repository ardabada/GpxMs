using GpxMs.GeoService.Application.Models;
using GpxMs.GeoService.Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace GpxMs.GeoService.Application.Queries
{
    public class AnalyzeTrackQuery : IRequest<TrackAnalyzationResult>
    {
        public List<TimedTrack> Tracks { get; private set; }

        public AnalyzeTrackQuery(List<TimedTrack> tracks)
        {
            this.Tracks = tracks;
        }
    }
}
