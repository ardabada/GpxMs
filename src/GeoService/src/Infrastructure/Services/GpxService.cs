using GpxMs.GeoService.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GpxMs.GeoService.Infrastructure.Services
{
    public interface IGpxService
    {
        XDocument GenerateGpx(List<TimedTrack> data);
    }
    public class GpxService : IGpxService
    {
        public XDocument GenerateGpx(List<TimedTrack> data)
        {
            List<XElement> tracks = new List<XElement>();
            foreach (var track in data)
            {
                List<XElement> elements = new List<XElement>();
                foreach (var time in track)
                {
                    elements.Add(new XElement("trkpt",
                                    new XAttribute("lat", time.Lat),
                                    new XAttribute("lon", time.Long),
                                    new XElement("time", time.Time.ToString("s") + "Z")));
                }
                tracks.Add(new XElement("trkseg", from i in elements select i));
            }
            XDocument doc = new XDocument(
                new XElement("gpx",
                new XAttribute("version", "1.1"),
                new XAttribute("creator", "Strava Manual Activity Generator"),
                    new XElement("trk",
                        from i in tracks select i)));
            return doc;
        }
    }
}
