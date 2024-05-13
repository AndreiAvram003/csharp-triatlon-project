using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace Repository.connectionUtils
{
    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection createConnection(IDictionary<string, string> props)
        {
            // Extrage șirul de conexiune din props
            string connectionString = props["ConnectionString"];

            // Creează o conexiune SQLite cu șirul de conexiune extras
            return new SqliteConnection(connectionString);
        }
    }
}