using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MLQuasar.Application.Services;
using MLQuasar.Application.Services.Interfaces;
using MLQuasar.Domain.Repositories;
using MLQuasar.Infrastructure;
using MLQuasar.Infrastructure.Repositories;
using MLQuasar.Services;

namespace MLQuasar.Api
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
            services.AddSingleton<IDBContext, DBContext>();
            services.AddTransient<IQuasarRepository, QuasarRepository>();
            services.AddTransient<ITrilaterationService, TrilaterationService>();
            services.AddTransient<ICommunicationService, CommunicationService>();
            services.AddTransient<IDecoderService, DecoderService>();
            services.AddTransient<ISatelliteService, SatelliteService>();

            
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"MLQuasar {groupName}",
                    Version = groupName,
                    Description = "MLQuasar API",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MLQuasar V1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
