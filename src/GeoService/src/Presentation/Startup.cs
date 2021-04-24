using GpxMs.GeoService.Application.Handlers;
using GpxMs.GeoService.Infrastructure.gRPC;
using GpxMs.GeoService.Infrastructure.gRPC.GpxRegistryService;
using GpxMs.GeoService.Infrastructure.gRPC.VisualizationService;
using GpxMs.GeoService.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GpxMs.GeoService.Presentation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddGrpcReflection();
            services.AddMediatR(typeof(ExtendRouteHandler).Assembly);

            services.Configure<GrpcServicesSettings>(Configuration.GetSection(nameof(GrpcServicesSettings)));

            services.AddSingleton<IGrpcChannelPool, GrpcChannelPool>();
            services.AddSingleton<IVisualizationServiceClient, VisualizationServiceClient>();
            services.AddSingleton<IGpxRegistryServiceClient, GpxRegistryServiceClient>();
            services.AddTransient<IGpxService, GpxService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<Services.GeoService>();
                
                if (env.IsDevelopment())
                {
                    endpoints.MapGrpcReflectionService();
                }

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Geo Service is running.");
                });
            });
        }
    }
}
