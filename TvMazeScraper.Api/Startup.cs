using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TvMaze.Connector.Configuration;
using TvMaze.Connector.Extensions;
using TvMazeScraper.Api.Entities;
using TvMazeScraper.Api.Mappers;
using TvMazeScraper.Api.Services;
using TvMazeScraper.Core.MiddleWare.ExceptionMiddleware;

namespace TvMazeScraper.Api
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
            services.AddControllers();
            services.AddTransient<ITvMazeService, TvMazeService>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTvMazeServices(Configuration);

            var connectionString = Configuration.GetConnectionString("TvMazeDatabase");
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApiDbContext>(o => o.UseNpgsql(connectionString));

            services.Configure<TvMazeOptions>(Configuration.GetSection(nameof(TvMazeOptions)));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}