using GpxMs.GeoService.Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace GpxMs.GeoService.Application.Commands
{
    public class SaveTimedTrackCommand : IRequest<string>
    {
        public List<TimedTrack> Tracks { get; private set; }

        public SaveTimedTrackCommand(List<TimedTrack> tracks)
        {
            this.Tracks = tracks;
        }
    }
}
