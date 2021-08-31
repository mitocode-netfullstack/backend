using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using mitocode.netfullstack.dataaccess;
using mitocode.netfullstack.services;
using MitoCode.WebApi.Filter;

namespace MitoCode.WebApi
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", false, true);

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        private IConfiguration Configuration { get; }
        private readonly string _corsMitoCode = "_corsMitoCode";


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddCors(options =>
                    options.AddPolicy(_corsMitoCode,
                        builder =>
                        {
                            // builder.WithOrigins("*", "http://localhost","https://localhost");
                            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        }
                    ))
                .AddMvcCore()
                .AddApiExplorer();

            services.AddInjection();

            services.AddDbContext<MitoCodeDbContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("Default")); });

            services.AddControllers(options => { options.Filters.Add<MitoCodeFilterException>(); });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MitoCode.WebApi",
                    Version = "v1",
                    Description = "API para Mitocode Net Full Stack"
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

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MitoCode.WebApi v1"));

            app.UseHttpsRedirection();
            app.UseCors(_corsMitoCode);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}