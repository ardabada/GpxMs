using GpxMs.GeoService.Domain.Models;
using GpxMs.GeoService.Application.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GpxMs.GeoService.Application.Handlers
{
    public class ExtendRouteHandler : IRequestHandler<ExtendRouteQuery, List<Track>>
    {
        public Task<List<Track>> Handle(ExtendRouteQuery request, CancellationToken cancellationToken)
        {
            List<Track> resultTracks = new List<Track>();
            List<Coord> finalTrackCoords = new List<Coord>();
            List<Coord> points = new List<Coord>();
            double step = request.Step;

            foreach (var track in request.Tracks)
            {
                cancellationToken.ThrowIfCancellationRequested();

                finalTrackCoords.Clear();

                for (int i = 0; i < track.Count - 1; i++)
                {
                    Coord p1 = track[i], p2 = track[i + 1];
                    bool backDirection = false;
                    if (p1.Lat > p2.Lat)
                    {
                        backDirection = true;
                        p1 = p2;
                        p2 = track[i];
                    }

                    double a = (p1.Long - p2.Long) / (p1.Lat - p2.Lat);
                    double b = p2.Long - p2.Lat * a;

                    points.Clear();
                    for (double j = p1.Lat + step; j < p2.Lat; j += step)
                    {
                        double y = a * j + b;
                        points.Add(new Coord(j, y));
                    }
                    if (backDirection)
                    {
                        points.Reverse(); 
                        finalTrackCoords.Add(p2);
                    }
                    else finalTrackCoords.Add(p1);

                    finalTrackCoords.AddRange(points);
                }

                resultTracks.Add(new Track(finalTrackCoords));
            }

            return Task.FromResult(resultTracks);
        }
    }
}
