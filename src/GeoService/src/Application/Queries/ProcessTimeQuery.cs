using GpxMs.GeoService.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace GpxMs.GeoService.Application.Queries
{
    public class ProcessTimeQuery : IRequest<List<TimedTrack>>
    {
        public List<Track> Tracks { get; }
        public DateTime Start { get; }
        public List<DateTime> Splits { get; }

        public ProcessTimeQuery(List<Track> tracks, DateTime start, List<DateTime> splits)
        {
            Tracks = tracks;
            Start = start;
            Splits = splits;
        }
    }
}
