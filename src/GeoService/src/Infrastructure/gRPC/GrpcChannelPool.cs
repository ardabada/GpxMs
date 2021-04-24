using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using System;

namespace GpxMs.GeoService.Infrastructure.gRPC
{
    public interface IGrpcChannelPool
    {
        GrpcChannel VisualizationServiceChannel { get; }
        GrpcChannel GpxRegistryServiceChannel { get; }
    }

    public class GrpcChannelPool : IGrpcChannelPool
    {
        public GrpcChannelPool(IOptionsMonitor<GrpcServicesSettings> settings)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            VisualizationServiceChannel = GrpcChannel.ForAddress(settings.CurrentValue.VisualizationServiceHost);
            GpxRegistryServiceChannel = GrpcChannel.ForAddress(settings.CurrentValue.GpxRegistryServiceHost);
        }

        public GrpcChannel VisualizationServiceChannel { get; private set; }

        public GrpcChannel GpxRegistryServiceChannel { get; private set; }
    }
}
