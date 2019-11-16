using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PubSubWorkerStarter.Domain.DataManager;
using PubSubWorkerStarter.Helpers;
using PubSubWorkerStarter.Services;
using PubSubWorkerStarter.Subscribers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            //Register services in Installers
            services.AddServicesInAssembly(_configuration);

            services.AddTransient<IPubSubService, PubSubService>();

            /// Register the unit of work persistence
            /// Make sure to use the service registration factory to insert a connection string
            var connectionString = _configuration.GetConnectionString("database");

            /// Register the user manager
            services.AddTransient<IUserManager, UserManager>();

            /// Register the subscription job
            services.AddHostedService<UserCreationSubscriber>();
        }
    }
}
