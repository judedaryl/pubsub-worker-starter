using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PubSubWorkerStarter.Contracts;
using PubSubWorkerStarter.Data.DataManager;
using PubSubWorkerStarter.Services;

namespace PubSubWorkerStarter.Infrastructure.Installers
{
    internal class RegisterContracts : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {

            /// Register the PubSubService to be used by the subscribers
            /// You can add more of these but make sure to use the write one in your subscriber
            services.AddTransient<IPubSubService, PubSubService>();


            /// Register the user manager
            /// Managers usually have a connection to the database layer
            /// In this template, we are going to use IUnitOfWork
            /// which is registered and configured using 
            /// 
            /// services.AddDatabase(q => {
            ///     q.UseNpgsql("myconnectionString");
            /// });
            services.AddTransient<IUserManager, UserManager>();
        }
    }
}
