using GpxMs.GeoService.Application.Models;
using GpxMs.GeoService.Application.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GpxMs.GeoService.Application.Handlers
{
    public class ProcessTimeHandler : IRequestHandler<ProcessTimeQuery, List<TimedTrack>>
    {
        public Task<List<TimedTrack>> Handle(ProcessTimeQuery request, CancellationToken cancellationToken)
        {
            var _timedCoords = new List<LinearTimedTrack>();
            for (int i = 0; i < request.Splits.Count; i++)
            {
                LinearTimedTrack track = new LinearTimedTrack();
                track.StartTime = i == 0 ? request.Start : request.Splits[i - 1];
                track.EndTime = request.Splits[i];
                track.AddRange(request.Tracks[i]);
                _timedCoords.Add(track);
            }

            List<TimedTrack> data = new List<TimedTrack>();
            foreach (var track in _timedCoords)
            {
                List<TimedCoord> timed = track.ConvertAll(x => new TimedCoord(x));
                var startUTC = track.StartTime.ToUniversalTime();
                var endUTC = track.EndTime.ToUniversalTime();
                timed[0].Time = startUTC;
                timed[timed.Count - 1].Time = endUTC;
                TimeSpan fullTime = endUTC - startUTC;
                double distance = Track.GetDistance(track); //meters
                double pace = TimeSpan.FromSeconds(fullTime.TotalSeconds / distance).TotalSeconds; //pace per meter (seconds)
                double lastPace = pace;
                for (int i = 0; i < timed.Count - 1; i++)
                {
                    double distanceToPoint = Track.GetDistance(timed.Take(i + 1).ToList());
                    double time = distanceToPoint * pace;
                    timed[i].Time = startUTC.Add(TimeSpan.FromSeconds(time));
                }
                data.Add(new TimedTrack(timed));
            }

            return Task.FromResult(data);
        }

    }
}
