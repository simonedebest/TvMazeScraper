using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TvMaze.Connector.Configuration;

 namespace TvMaze.Connector.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTvMazeServices(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection(nameof(TvMazeOptions)).Get<TvMazeOptions>();
            services.AddHttpClient<ITvMazeConnector, TvMazeConnector>(client =>
            {
                client.BaseAddress = new Uri(options.BaseUri);
            });
        }
    }
}