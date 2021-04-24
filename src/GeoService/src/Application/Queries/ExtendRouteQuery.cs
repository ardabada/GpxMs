using GpxMs.GeoService.Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace GpxMs.GeoService.Application.Queries
{
    public class ExtendRouteQuery : IRequest<List<Track>>
    {
        public List<Track> Tracks { get; }
        public double Step { get; }

        public ExtendRouteQuery(List<Track> tracks, double step)
        {
            Tracks = tracks;
            Step = step;
        }
    }
}
