using Microsoft.Data.Sqlite;

namespace PasswordManager.dal;

public class DatabaseManager {
    private const string ConnectionString = "Data Source=wow.db";

    public DatabaseManager() {
        CreateAccountsTableIfNotExists();
        CreateMasterKeyTableIfNotExists();
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
        
    // Method to create the MasterKey table if it doesn't exist
    private void CreateMasterKeyTableIfNotExists() {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
                CREATE TABLE IF NOT EXISTS MasterKey (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    HashedKey TEXT NOT NULL,
                    Salt TEXT NOT NULL
                );";
        command.ExecuteNonQuery();
    }
}