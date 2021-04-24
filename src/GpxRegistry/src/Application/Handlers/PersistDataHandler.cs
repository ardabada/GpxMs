using Application.Commands;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class PersistDataHandler : IRequestHandler<PersistDataCommand, bool>
    {
        private readonly IOptionsMonitor<DataPersistOptions> options;

        public PersistDataHandler(IOptionsMonitor<DataPersistOptions> options)
        {
            this.options = options;
        }

        public async Task<bool> Handle(PersistDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string dir = Path.Combine(options.CurrentValue.Location, DateTime.Now.ToString("yyyyMMdd"));
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string fileName = request.Name + "_" + Guid.NewGuid() + "." + request.Type;
                await File.WriteAllBytesAsync(Path.Combine(dir, fileName), request.Data, cancellationToken);
                return true;
            }
            catch { return false; }
        }
    }
}
