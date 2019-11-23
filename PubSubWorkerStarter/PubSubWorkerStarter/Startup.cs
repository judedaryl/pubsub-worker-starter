using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PubSubWorkerStarter.Infrastructure.Extensions;

namespace PubSubWorkerStarter
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            /// Register the persistence layer
            /// This extension adds a IUnitOfWork to the IServiceCollection
            services.AddDatabase(q => {
                q.UseNpgsql(_configuration.GetConnectionString("PostGres"));
            });


            /// Register services in Installers
            /// This registers:
            /// * External endpoint data services
            /// * Contracts (Managers, Services)
            /// * Workers (PubSub Subscribers)
            services.AddServicesInAssembly(_configuration);        

        }
    }
}
