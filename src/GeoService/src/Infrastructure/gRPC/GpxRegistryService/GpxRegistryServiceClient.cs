using System.Threading.Tasks;

namespace GpxMs.GeoService.Infrastructure.gRPC.GpxRegistryService
{
    public interface IGpxRegistryServiceClient
    {
        Task<bool> PersistData(byte[] data, string name, string type);
    }
    public class GpxRegistryServiceClient : IGpxRegistryServiceClient
    {
        private readonly Protos.GpxRegistryService.GpxRegistryServiceClient client;

        public GpxRegistryServiceClient(IGrpcChannelPool grpcChannelPool)
        {
            client = new Protos.GpxRegistryService.GpxRegistryServiceClient(grpcChannelPool.GpxRegistryServiceChannel);
        }

        public async Task<bool> PersistData(byte[] data, string name, string type)
        {
            return (await client.PersistDataAsync(new Protos.PersistDataRequestMessage()
            {
                Data = Google.Protobuf.ByteString.CopyFrom(data),
                NamePrefix = name,
                Type = type
            })).Saved;
        }
    }
}
