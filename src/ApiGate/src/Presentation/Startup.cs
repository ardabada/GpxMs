using GpxMs.ApiGate.Application.Handlers;
using GpxMs.ApiGate.Infrastructure.gRPC;
using GpxMs.ApiGate.Infrastructure.gRPC.GeoService;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpxMs.ApiGate.Presentation
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
            services.AddControllers();

            services.AddSwaggerGen();

            services.Configure<GrpcServicesSettings>(Configuration.GetSection(nameof(GrpcServicesSettings)));

            services.AddSingleton<IGrpcChannelPool, GrpcChannelPool>();
            services.AddSingleton<IGeoServiceClient, GeoServiceClient>();

            services.AddMediatR(typeof(BuildAndAnalyzeHandler).Assembly);
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
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Api Gateway is running.");
                });
            });

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
