using System;

namespace GpxMs.ApiGate.Domain.Models
{
    public class Coord
    {
        public Coord() { }
        public Coord(double lat, double lng)
        {
            Lat = lat;
            Long = lng;
        }

        public double Lat { get; set; }
        public double Long { get; set; }
    }

    public class TimedCoord : Coord
    {
        public TimedCoord() { }
        public TimedCoord(Coord source) : this(source.Lat, source.Long) { }
        public TimedCoord(double lat, double lng) : base(lat, lng) { }
        public TimedCoord(double lat, double lng, DateTime time) : base(lat, lng)
        {
            Time = time;
        }
        public DateTime Time { get; set; }
    }
}
