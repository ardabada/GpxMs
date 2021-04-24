using GpxMs.ApiGate.Application.Commands;
using GpxMs.ApiGate.Domain.Models.Requests;
using GpxMs.ApiGate.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GpxMs.ApiGate.Presentation.Controllers
{
    public class GpxController : ControllerBase
    {
        private readonly ILogger<GpxController> logger;
        private readonly IMediator mediator;

        public GpxController(ILogger<GpxController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpPost("build")]
        public async Task<BuildAndAnalyzeResponse> BuildAndAnalyze([FromBody] BuildAndAnalyzeRequest request)
        {
            logger.LogInformation("Requested coordinates extension");

            var mediatorRequest = new BuildAndAnalyzeCommand(request);
            var result = await mediator.Send(mediatorRequest);

            return result;
        }
    }
}
