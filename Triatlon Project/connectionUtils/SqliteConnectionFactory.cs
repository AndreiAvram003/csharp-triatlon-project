using System.Data;
using Microsoft.Data.Sqlite;

namespace Triatlon_Project.connectionUtils;

public class SqliteConnectionFactory : ConnectionFactory
{
    public override IDbConnection createConnection(IDictionary<string,string> props)
    {
        //Mono Sqlite Connection

        //String connectionString = "URI=file:/Users/grigo/didactic/MPP/ExempleCurs/2017/database/tasks.db,Version=3";
        // String connectionString = props["ConnectionString"];
        // Console.WriteLine("SQLite ---Se deschide o conexiune la  ... {0}", connectionString);
        // return new SqliteConnection(connectionString);
        
        String connectionString = "Data Source=tasks.db;Version=3";
        return new SqliteConnection(connectionString);
    }
}