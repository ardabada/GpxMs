namespace GpxMs.ApiGate.Domain.Models.Requests
{
    public class TrackExtentionRequest
    {
        public Track Track { get; set; }
        public double Step { get; set; }
    }
}
