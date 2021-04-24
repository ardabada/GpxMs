using GpxMs.ApiGate.Domain.Models.Requests;
using GpxMs.ApiGate.Domain.Models.Responses;
using MediatR;

namespace GpxMs.ApiGate.Application.Commands
{
    public class BuildAndAnalyzeCommand : IRequest<BuildAndAnalyzeResponse>
    {
        public BuildAndAnalyzeRequest Request { get; private set; }

        public BuildAndAnalyzeCommand(BuildAndAnalyzeRequest request)
        {
            this.Request = request;
        }
    }
}
