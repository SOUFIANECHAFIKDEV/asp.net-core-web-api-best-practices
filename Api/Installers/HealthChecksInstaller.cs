using Api.Data;
using Api.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Installers
{
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddHealthChecks()
                    .AddDbContextCheck<DataContext>()
                    .AddCheck<RedisHealthCheck>("Redis"); // Redis is the component name
        }
    }
}