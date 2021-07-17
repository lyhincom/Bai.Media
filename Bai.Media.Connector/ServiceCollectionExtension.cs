using Bai.Media.Connector.Abstractions;
using Bai.Media.Connector.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bai.General.Swagger
{
    public static class ServiceCollectionExtension
    {
        public static void AddMediaConnector(this IServiceCollection services) =>
            services.AddTransient<IMediaConnector, MediaConnector>();
    }
}
