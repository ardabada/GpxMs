using Application.Commands;
using GpxMs.GpxRegistry.Presentation.Protos;
using Grpc.Core;
using MediatR;
using System.Threading.Tasks;

namespace GpxMs.GpxRegistry.Presentation.Services
{
    public class GpxRegistryService : Protos.GpxRegistryService.GpxRegistryServiceBase
    {
        private readonly IMediator mediator;

        public GpxRegistryService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<PersistDataResponseMessage> PersistData(PersistDataRequestMessage request, ServerCallContext context)
        {
            var mediatorRequest = new PersistDataCommand(request.Data.ToByteArray(), request.Type, request.NamePrefix);
            var mediatorResponse = await mediator.Send(mediatorRequest);
            return new PersistDataResponseMessage() { Saved = mediatorResponse };
        }
    }
}
