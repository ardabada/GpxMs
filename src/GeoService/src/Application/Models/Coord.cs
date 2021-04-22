namespace GpxMs.GeoService.Application.Models
{
    public class Coord
    {
        public Coord(double lat, double lng)
        {
            this.Lat = lat;
            this.Long = lng;
        }

        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
