using System.Collections.Generic;

namespace GpxMs.ApiGate.Domain.Models
{
    public class TrackBase<T> where T : Coord
    {
        public List<T> Coords { get; set; }
    }
    public class Track : TrackBase<Coord> { }
    public class TimedTrack : TrackBase<TimedCoord> { }
}
