using Microsoft.Extensions.DependencyInjection;
using PubSubWorkerStarter.Contracts;
using PubSubWorkerStarter.Data;
using System;

namespace PubSubWorkerStarter.Infrastructure.Extensions
{
    public static class PersistenceExtension
    {
        public static void AddDatabase(this IServiceCollection services, Action<IDatabaseConfig> builder)
        {
            var dbConfig = new DatabaseConfig();
            builder(dbConfig);

            if (dbConfig.GetConnectionFactory() == null) throw new ArgumentNullException($"Connection factory is null");

            /// Add database config interface so it can be injected to UnitOfWork
            services.AddTransient<IDatabaseConfig>(_ => dbConfig);

            /// Previously implemented using a factory. Adding as an assembly reference instead
            /// to support injection of services from IServiceCollection
            /// Configuration is then passed as an interface instead using IDatabaseConfig
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}