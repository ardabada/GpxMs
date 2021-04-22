using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using System;

namespace GpxMs.ApiGate.Infrastructure.gRPC
{
    public interface IGrpcChannelPool
    {
        GrpcChannel GeoServiceChannel { get; }
    }

    public class GrpcChannelPool : IGrpcChannelPool
    {
        public GrpcChannelPool(IOptionsMonitor<GrpcServicesSettings> settings)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            GeoServiceChannel = GrpcChannel.ForAddress(settings.CurrentValue.GeoServiceHost);
        }

        public GrpcChannel GeoServiceChannel { get; private set; }
    }
}
