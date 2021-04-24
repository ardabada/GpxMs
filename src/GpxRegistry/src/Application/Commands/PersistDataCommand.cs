using MediatR;

namespace Application.Commands
{
    public class PersistDataCommand : IRequest<bool>
    {
        public byte[] Data { get; }
        public string Type { get; }
        public string Name { get; }

        public PersistDataCommand(byte[] data, string type, string name)
        {
            Data = data;
            Type = type;
            Name = name;
        }
    }
}
