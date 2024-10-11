using Microsoft.Data.Sqlite;
using PasswordManager.be;
using PasswordManager.bll;

namespace PasswordManager.dal;

public class AccountDAO {
    private readonly string _connectionString = "Data Source=wow.db";
    private readonly EncryptionService _encryptionService;

    public AccountDAO(string password) {
        _encryptionService = new EncryptionService(password);
    }

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
                    Provider = _encryptionService.Decrypt(reader.GetString(1)),
                    Username = _encryptionService.Decrypt(reader.GetString(2)),
                    Password = _encryptionService.Decrypt(reader.GetString(3))
                });
            }
        }

        return accounts;
    }

    public Account InsertAccount(Account account) {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = @"
        INSERT INTO Accounts (Provider, Username, Password)
        VALUES ($provider, $username, $password);
        SELECT last_insert_rowid();";

        command.Parameters.AddWithValue("$provider", _encryptionService.Encrypt(account.Provider));
        command.Parameters.AddWithValue("$username", _encryptionService.Encrypt(account.Username));
        command.Parameters.AddWithValue("$password", _encryptionService.Encrypt(account.Password));

        account.Id = Convert.ToInt32(command.ExecuteScalar());

        return account;
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
