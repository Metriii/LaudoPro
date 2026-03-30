using Microsoft.Data.Sqlite;

namespace ApiWeb.Data
{
    public class DatabaseContext
    {
        private readonly string connectionString = "Data Source=LaudoPro.db;Version=3;";

        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }
    }
}