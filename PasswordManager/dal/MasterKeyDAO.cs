using Microsoft.Data.Sqlite;

namespace PasswordManager.dal {
    public class MasterKeyDAO {
        private readonly string _connectionString = "Data Source=wow.db";

        public void InsertMasterKey(string hashedKey, string salt) {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO MasterKey (HashedKey, Salt)
                VALUES ($hashedKey, $salt);";
            command.Parameters.AddWithValue("$hashedKey", hashedKey);
            command.Parameters.AddWithValue("$salt", salt);
            command.ExecuteNonQuery();
        }

        public (string HashedKey, string Salt)? GetMasterKey() {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT HashedKey, Salt FROM MasterKey LIMIT 1;";

            using (var reader = command.ExecuteReader()) {
                if (reader.Read()) {
                    string hashedKey = reader.GetString(0);
                    string salt = reader.GetString(1);
                    return (hashedKey, salt);
                }
            }
            return null;
        }

        public bool MasterKeyExists() {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM MasterKey;";
            var count = (long)command.ExecuteScalar();
            return count > 0;
        }
    }
}
