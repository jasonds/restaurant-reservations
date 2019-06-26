using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Reservations.Application.Services;
using Restaurant.Reservations.Core.Services;
using Restaurant.Reservations.Infrastructure.Data;
using Restaurant.Reservations.Infrastructure.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace Restaurant.Reservations.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Restaurant Reservations API", Version = "v1" });

                // make Swagger work with XML comments
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var commentsFileName = Assembly.GetEntryAssembly().GetName().Name + ".xml";
                var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                if (File.Exists(commentsFile)) c.IncludeXmlComments(commentsFile, true);
                c.DescribeAllEnumsAsStrings();
                c.CustomSchemaIds(i => i.FullName);
            });
            
            // Configure dependency injection
            DependencyInjection(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant Reservations API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void DependencyInjection(IServiceCollection services)
        {
            // Register DbContext
            services.AddDbContext<ReservationDbContext>(x => x
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging(true)
                .ConfigureWarnings(cw => cw.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            );

            // contexts
            services.AddHttpContextAccessor();

            // services
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IReservationProvider, OpenTableReservationProvider>();
        }
    }
}
