namespace PasswordManager.be;

public class Account {
    public int Id { get; set; }
    public string Provider { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    // Computed property to return obfuscated password
    public string MaskedPassword => new string('*', 8); // Show 8 stars for the password

    // Computed property to return obfuscated username
    public string MaskedUsername {
        get {
            if (string.IsNullOrEmpty(Username))
                return string.Empty;
            return $"{Username[0]}..."; // Show first letter followed by '...'
        }
    }
}