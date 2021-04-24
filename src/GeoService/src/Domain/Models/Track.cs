using System;
using System.Collections.Generic;

namespace GpxMs.GeoService.Domain.Models
{
    public class TrackBase<T> : List<T> where T : Coord
    {
        public TrackBase() : base() { }
        public TrackBase(List<T> coords) : base(coords) { }
    }

    public class Track : TrackBase<Coord>
    {
        public Track() : base() { }
        public Track(List<Coord> coords) : base(coords) { }

        public static double GetDistance<T>(List<T> source) where T : Coord
        {
            double result = 0;
            for (int i = 0; i < source.Count - 1; i++)
                result += source[i].Distance(source[i + 1]);
            return result;
        }
    }

    public class TimedTrack : TrackBase<TimedCoord>
    {
        public TimedTrack() : base() { }
        public TimedTrack(List<TimedCoord> coords) : base(coords) { }
    }

    public class LinearTimedTrack : Track
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
