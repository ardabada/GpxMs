using System.Collections.Generic;

namespace GpxMs.GeoService.Application.Models
{
    public class Track
    {
        public Track(List<Coord> coords)
        {
            this.Coords = coords;
        }

        public List<Coord> Coords { get; set; }
    }
}
