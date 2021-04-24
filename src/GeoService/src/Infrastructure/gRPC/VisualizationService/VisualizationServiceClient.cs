using GpxMs.GeoService.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpxMs.GeoService.Infrastructure.gRPC.VisualizationService
{
    public interface IVisualizationServiceClient
    {
        Task<byte[]> GetPathImage(List<Coord> coords, string pathColor, int pathWidth, int imageWidth, int imageHeight);
    }

    public class VisualizationServiceClient : IVisualizationServiceClient
    {
        private readonly Protos.VisualizationService.VisualizationServiceClient client;

        public VisualizationServiceClient(IGrpcChannelPool grpcChannelPool)
        {
            client = new Protos.VisualizationService.VisualizationServiceClient(grpcChannelPool.VisualizationServiceChannel);
        }

        public async Task<byte[]> GetPathImage(List<Coord> coords, string pathColor, int pathWidth, int imageWidth, int imageHeight)
        {
            var request = new Protos.RouteVisualizationRequestMessage();
            request.Coords.AddRange(coords.Select(x => new Protos.CoordMessage()
            {
                Lat = x.Lat,
                Long = x.Long
            }));
            request.PathColor = pathColor;
            request.PathWidth = pathWidth;
            request.ImageWidth = imageWidth;
            request.ImageHeight = imageHeight;
            var result = await client.GetPathImageAsync(request);
            return result.Image.ToByteArray();
        }
    }
}
