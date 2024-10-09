
using Microsoft.Data.Sqlite;
using PasswordManager.be;

namespace PasswordManager.dal;

public class AccountDAO {
    private readonly string _connectionString = "Data Source=accounts.db";

    public List<Account> GetAllAccounts() {
        var accounts = new List<Account>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Provider, Username, Password FROM Accounts";

        using (var reader = command.ExecuteReader()) {
            while (reader.Read()) {
                accounts.Add(new Account {
                    Id = reader.GetInt32(0),
                    Provider = reader.GetString(1),
                    Username = reader.GetString(2),
                    Password = reader.GetString(3)
                });
            }
        }

        return accounts;
    }

    public void InsertAccount(Account account) {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
                    INSERT INTO Accounts (Provider, Username, Password)
                    VALUES ($provider, $username, $password)";
        command.Parameters.AddWithValue("provider", account.Provider);
        command.Parameters.AddWithValue("$username", account.Username);
        command.Parameters.AddWithValue("$password", account.Password);
        command.ExecuteNonQuery();
    }

    public void UpdateAccount(Account account) {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
                    UPDATE Accounts 
                    SET Provider = $provider, Username = $username, Password = $password
                    WHERE Id = $id";
        command.Parameters.AddWithValue("provider", account.Provider);
        command.Parameters.AddWithValue("$username", account.Username);
        command.Parameters.AddWithValue("$password", account.Password);
        command.Parameters.AddWithValue("$id", account.Id);
        command.ExecuteNonQuery();
    }

    public void DeleteAccount(int accountId) {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Accounts WHERE Id = $id";
        command.Parameters.AddWithValue("$id", accountId);
        command.ExecuteNonQuery();
    }
}
