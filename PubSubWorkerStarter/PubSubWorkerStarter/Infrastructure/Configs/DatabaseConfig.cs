using PubSubWorkerStarter.Contracts;
using System;
using System.Data.Common;

namespace PubSubWorkerStarter
{
    public class DatabaseConfig : IDatabaseConfig
    {
        private Func<DbConnection> ConnectionFactory { get; set; }

        public Func<DbConnection> GetConnectionFactory() => ConnectionFactory;

        public void UseNpgsql(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException($"Connection string is null");
            ConnectionFactory = () => new Npgsql.NpgsqlConnection(connectionString);
        }
    }
}
