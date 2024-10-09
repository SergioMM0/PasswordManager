using Microsoft.Data.Sqlite;

namespace PasswordManager.dal {
    public class DatabaseManager {
        private const string ConnectionString = "Data Source=accounts.db";

        public DatabaseManager() {
            CreateAccountsTableIfNotExists();
        }

        private void CreateAccountsTableIfNotExists() {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Accounts (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Provider TEXT NOT NULL,
                        Username TEXT NOT NULL,
                        Password TEXT NOT NULL
                    );";
            command.ExecuteNonQuery();
        }
    }
}
