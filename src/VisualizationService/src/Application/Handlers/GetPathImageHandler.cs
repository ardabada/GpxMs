using GpxMs.VisualizationService.Application.Models;
using GpxMs.VisualizationService.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GpxMs.VisualizationService.Application.Handlers
{
    public class GetPathImageHandler : IRequestHandler<GetPathImageQuery, RouteVisualizationResponse>
    {
        private readonly ILogger<GetPathImageHandler> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public GetPathImageHandler(ILogger<GetPathImageHandler> logger,
            IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<RouteVisualizationResponse> Handle(GetPathImageQuery request, CancellationToken cancellationToken)
        {
            var _request = request.Request;
            var startPoint = _request.Coords.First();
            var lastPoint = _request.Coords.Last();

            const int max_coords = 42;

            var coords = FilterCoords(_request.Coords, max_coords);
            var coords_str = string.Join(",", coords.Select(x => x.ToString()));

            string url = $"https://tc.mobile.yandex.net/get-map/1.x/?lang=ru&lg=0&scale=1&pt={ startPoint },comma_solid_red~{ lastPoint },comma_solid_blue&l=map&cr=0&pl=c:{ _request.PathColor },w:{ _request.PathWidth },{coords_str}&size={_request.ImageWidth},{_request.ImageHeight}";

            byte[] imageBytes = null;
            try
            {
                imageBytes = await httpClientFactory.CreateClient().GetByteArrayAsync(url);
            }
            catch { logger.LogError("Unable to receive image from {url}", url); }

            return new RouteVisualizationResponse { Image = imageBytes }; 
        }

        private IEnumerable<Coord> FilterCoords(List<Coord> source, int maxNumber)
        {
            var indexes = LinspaceIndexes(0, source.Count, maxNumber);
            for (int i = 0; i < indexes.Length; i++)
            {
                yield return source[indexes[i]];
            }
        }

        private int[] LinspaceIndexes(int start, int end, int n)
        {
            float interval = (float)(start + end) / (n - 1);
            return Enumerable.Range(0, n).Select(x => (int)(start + (x * interval))).Distinct().ToArray();
        }
    }
}
