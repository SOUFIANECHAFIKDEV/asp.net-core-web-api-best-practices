using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Installers
{
    public interface IInstaller
    {
        void InstallerServices(IServiceCollection services, IConfiguration Configuration);
    }
}