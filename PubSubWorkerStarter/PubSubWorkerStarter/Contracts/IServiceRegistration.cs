using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PubSubWorkerStarter.Contracts
{
    public interface IServiceRegistration
    {
        void RegisterAppServices(IServiceCollection services, IConfiguration configuration);
    }
}
