using System;
using System.Data.Common;

namespace PubSubWorkerStarter.Contracts
{
    public interface IDatabaseConfig
    {
        Func<DbConnection> GetConnectionFactory();
        void UseNpgsql(string connectionString);
        void UseSql(string connectionString);
    }
}
