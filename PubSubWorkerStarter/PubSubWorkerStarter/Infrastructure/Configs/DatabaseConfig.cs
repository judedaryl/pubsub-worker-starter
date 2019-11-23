using PubSubWorkerStarter.Contracts;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace PubSubWorkerStarter
{
    internal class DatabaseConfig : IDatabaseConfig
    {
        private Func<DbConnection> ConnectionFactory { get; set; }

        public Func<DbConnection> GetConnectionFactory() => ConnectionFactory;

        public void UseNpgsql(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException($"Connection string is null");
            ConnectionFactory = () => new Npgsql.NpgsqlConnection(connectionString);
        }

        public void UseSql(string connectionString)
        {
            ConnectionFactory = () => new SqlConnection(connectionString);
        }
    }
}
