using System;
using System.Collections.Generic;

namespace GpxMs.ApiGate.Domain.Models.Requests
{
    public class BuildAndAnalyzeRequest
    {
        public List<Track> Tracks { get; set; }
        public DateTime Start { get; set; }
        public List<DateTime> Splits { get; set; }
    }
}
