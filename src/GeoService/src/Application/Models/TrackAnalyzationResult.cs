using System.Collections.Generic;

namespace GpxMs.GeoService.Application.Models
{
    public class TrackAnalyzationResult
    {
        public double AverageSpeed { get; set; }
        public List<double> AverageSpeedSplits { get; set; }
    }
}
