using System.Collections.Generic;

namespace GpxMs.ApiGate.Domain.Models.Requests
{
    public class ExtendRouteRequest
    {
        public List<TrackExtentionRequest> Tracks { get; set; }
    }
}
