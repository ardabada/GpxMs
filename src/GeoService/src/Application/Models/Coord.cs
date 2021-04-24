using System;

namespace GpxMs.GeoService.Application.Models
{
    public class Coord
    {
        public Coord(double lat, double lng)
        {
            Lat = lat;
            Long = lng;
        }

        public double Lat { get; set; }
        public double Long { get; set; }

        public double Distance(Coord other)
        {
            double num = Lat * (Math.PI / 180.0);
            double num2 = Long * (Math.PI / 180.0);
            double num3 = other.Lat * (Math.PI / 180.0);
            double num4 = other.Long * (Math.PI / 180.0) - num2;
            double num5 = Math.Pow(Math.Sin((num3 - num) / 2.0), 2.0) + Math.Cos(num) * Math.Cos(num3) * Math.Pow(Math.Sin(num4 / 2.0), 2.0);
            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(num5), Math.Sqrt(1.0 - num5)));
        }
    }

    public class TimedCoord : Coord
    {
        public TimedCoord(Coord source) : this(source.Lat, source.Long) { }
        public TimedCoord(double lat, double lng) : base(lat, lng) { }
        public TimedCoord(double lat, double lng, DateTime time) : base(lat, lng)
        {
            Time = time;
        }
        public DateTime Time { get; set; }
    }
}
