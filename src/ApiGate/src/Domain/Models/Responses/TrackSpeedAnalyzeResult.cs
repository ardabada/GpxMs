using System.Collections.Generic;

namespace GpxMs.ApiGate.Domain.Models.Responses
{
    public class TrackSpeedAnalyzeResult
    {
        public double Average { get; set; }
        public List<double> Splits { get; set; }
    }
}
