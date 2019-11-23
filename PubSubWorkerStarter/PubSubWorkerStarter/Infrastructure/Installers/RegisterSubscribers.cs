using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PubSubWorkerStarter.Contracts;
using PubSubWorkerStarter.Subscribers;

namespace PubSubWorkerStarter.Infrastructure.Installers
{
    internal class RegisterSubscribers : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            /// Register the subscription job
            services.AddHostedService<UserCreationSubscriber>();
        }
    }
}
