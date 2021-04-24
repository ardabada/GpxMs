using GpxMs.GeoService.Application.Models;
using GpxMs.GeoService.Application.Queries;
using GpxMs.GeoService.Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GpxMs.GeoService.Application.Handlers
{
    public class AnalyzeTrackHandler : IRequestHandler<AnalyzeTrackQuery, TrackAnalyzationResult>
    {
        public Task<TrackAnalyzationResult> Handle(AnalyzeTrackQuery request, CancellationToken cancellationToken)
        {
            double totalDistance = 0;
            List<double> speedOnTracks = new List<double>();
            foreach (var track in request.Tracks)
            {
                double trackDistance = Track.GetDistance(track); //meters
                totalDistance += trackDistance;
                double seconds = (track[track.Count - 1].Time - track[0].Time).TotalSeconds;
                double speedKmsH = (trackDistance / 1000) / (seconds / 3600);
                speedOnTracks.Add(speedKmsH);
            }
            double totalTime = (request.Tracks.Last().Last().Time - request.Tracks.First().First().Time).TotalSeconds;
            double averageKmsH = (totalDistance / 1000) / (totalTime / 3600);
            return Task.FromResult(new TrackAnalyzationResult() { AverageSpeed = averageKmsH, AverageSpeedSplits = speedOnTracks });
        }
    }
}
