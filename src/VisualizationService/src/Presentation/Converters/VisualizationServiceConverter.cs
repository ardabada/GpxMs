using Google.Protobuf;
using GpxMs.VisualizationService.Application.Models;
using GpxMs.VisualizationService.Presentation.Protos;
using System.Linq;

namespace GpxMs.VisualizationService.Presentation.Converters
{
    public interface IVisualizationServiceConverter
    {
        Coord ConvertCoord(CoordMessage message);
        CoordMessage ConvertCoord(Coord model);

        RouteVisualizationRequest ConvertRouteVisualizationRequest(RouteVisualizationRequestMessage message);
        RouteVisualizationResponseMesage ConvertRouteVisualizationResponse(RouteVisualizationResponse model);
    }

    public class VisualizationServiceConverter : IVisualizationServiceConverter
    {
        public Coord ConvertCoord(CoordMessage message)
        {
            return new Coord(message.Lat, message.Long);
        }

        public CoordMessage ConvertCoord(Coord model)
        {
            return new CoordMessage()
            {
                Lat = model?.Lat ?? 0,
                Long = model?.Long ?? 0
            };
        }

        public RouteVisualizationRequest ConvertRouteVisualizationRequest(RouteVisualizationRequestMessage message)
        {
            return new RouteVisualizationRequest()
            {
                ImageHeight = message.ImageHeight,
                ImageWidth = message.ImageWidth,
                PathColor = message.PathColor,
                PathWidth = message.PathWidth,
                Coords = message.Coords.Select(x => ConvertCoord(x)).ToList()
            };
        }

        public RouteVisualizationResponseMesage ConvertRouteVisualizationResponse(RouteVisualizationResponse model)
        {
            return new RouteVisualizationResponseMesage()
            {
                Image = model.Image != null ? ByteString.CopyFrom(model.Image) : null
            };
        }
    }
}
