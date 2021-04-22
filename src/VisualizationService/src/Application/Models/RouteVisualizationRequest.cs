using GpxMs.VisualizationService.Domain;
using System.Collections.Generic;

namespace GpxMs.VisualizationService.Application.Models
{
    public class RouteVisualizationRequest
    {
        private int pathWidth = Consts.PATH_WIDTH_MIN;
        private int imageWidth = Consts.IMAGE_WIDTH_MIN;
        private int imageHeight = Consts.IMAGE_HEIGHT_MIN;

        public List<Coord> Coords { get; set; }
        public string PathColor { get; set; }
        public int PathWidth
        {
            get { return pathWidth; }
            set
            {
                if (value < Consts.PATH_WIDTH_MIN)
                    pathWidth = Consts.PATH_WIDTH_MIN;
                else if (value > Consts.PATH_WIDTH_MAX)
                    pathWidth = Consts.PATH_WIDTH_MAX;
                else pathWidth = value;
            }
        }
        public int ImageWidth
        {
            get { return imageWidth; }
            set
            {
                if (value < Consts.IMAGE_WIDTH_MIN)
                    imageWidth = Consts.IMAGE_WIDTH_MIN;
                else if (value > Consts.IMAGE_WIDTH_MAX)
                    imageWidth = Consts.IMAGE_WIDTH_MAX;
                else imageWidth = value;
            }
        }
        public int ImageHeight
        {
            get { return imageHeight; }
            set
            {
                if (value < Consts.IMAGE_HEIGHT_MIN)
                    imageHeight = Consts.IMAGE_HEIGHT_MIN;
                else if (value > Consts.IMAGE_HEIGHT_MAX)
                    imageHeight = Consts.IMAGE_HEIGHT_MAX;
                else imageHeight = value;
            }
        }
    }
}
