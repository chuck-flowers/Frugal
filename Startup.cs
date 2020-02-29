using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Frugal.Data;
using Frugal.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Frugal
{
    /// <summary>
    /// The class that is responsible for starting the web app.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor for the Startup class
        /// </summary>
        /// <param name="configuration">The web app configuration</param>
        public Startup(IConfiguration configuration) => Configuration = configuration;

        /// <summary>
        /// The configuraiton of the web app.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services that can be injected later.
        /// </summary>
        /// <param name="services">The collection the injectable services will be added to.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<FrugalContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("Frugal"));
            });

            services.AddAutoMapper(config => config.AddProfile(new MappingProfile()), Assembly.GetExecutingAssembly());

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Frugal API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configures the pipeline used by the web app.
        /// </summary>
        /// <param name="app">The application builder onto which middleware elements are added.</param>
        /// <param name="env">Information about the web host environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "Frugal API v1.0.0"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
