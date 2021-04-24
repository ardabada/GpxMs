using GpxMs.GeoService.Application.Commands;
using GpxMs.GeoService.Domain.Models;
using GpxMs.GeoService.Infrastructure.gRPC.GpxRegistryService;
using GpxMs.GeoService.Infrastructure.gRPC.VisualizationService;
using GpxMs.GeoService.Infrastructure.Services;
using MediatR;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GpxMs.GeoService.Application.Handlers
{
    public class SaveTimedTrackHandler : IRequestHandler<SaveTimedTrackCommand, string>
    {
        private readonly IVisualizationServiceClient visualizationServiceClient;
        private readonly IGpxRegistryServiceClient gpxRegistryServiceClient;
        private readonly IGpxService gpxService;

        public SaveTimedTrackHandler(IVisualizationServiceClient visualizationServiceClient,
            IGpxRegistryServiceClient gpxRegistryServiceClient,
            IGpxService gpxService)
        {
            this.visualizationServiceClient = visualizationServiceClient;
            this.gpxRegistryServiceClient = gpxRegistryServiceClient;
            this.gpxService = gpxService;
        }

        public async Task<string> Handle(SaveTimedTrackCommand request, CancellationToken cancellationToken)
        {
            string id = Guid.NewGuid().ToString();
            var coords = request.Tracks.SelectMany(x => x).Select(y => new Coord(y.Lat, y.Long)).ToList();
            var image = await visualizationServiceClient.GetPathImage(coords, "3C3C3C", 5, 500, 256);
            await gpxRegistryServiceClient.PersistData(image, id, "jpg");

            var gpxXml = gpxService.GenerateGpx(request.Tracks);
            byte[] gpxBytes = Encoding.UTF8.GetBytes(gpxXml.ToString());
            await gpxRegistryServiceClient.PersistData(gpxBytes, id, "gpx");

            return id;
        }
    }
}
