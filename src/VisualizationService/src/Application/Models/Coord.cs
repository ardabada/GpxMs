namespace GpxMs.VisualizationService.Application.Models
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

        public override string ToString()
        {
            return Long.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + Lat.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
