using System.Security.Cryptography;

namespace PasswordManager.util;

public static class TheTruePasswordManager {
    public static (string HashedPassword, string Salt) HashPassword(string password) {
        var saltBytes = new byte[16];

        RandomNumberGenerator.Fill(saltBytes);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256);
        var hashBytes = pbkdf2.GetBytes(32);

        var hashedPassword = Convert.ToBase64String(hashBytes);
        var salt = Convert.ToBase64String(saltBytes);

        return (hashedPassword, salt);
    }


    public static bool VerifyPassword(string enteredPassword, string storedHashedPassword, string storedSalt) {
        var saltBytes = Convert.FromBase64String(storedSalt);

        using var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000, HashAlgorithmName.SHA256);
        var hashBytes = pbkdf2.GetBytes(32);

        var enteredHashedPassword = Convert.ToBase64String(hashBytes);
        return enteredHashedPassword == storedHashedPassword;
    }

    public static string GenerateSecurePassword(int length) {
        const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
        var password = new char[length];
        var randomBytes = new byte[length];

        using (var rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(randomBytes);
        }

        for (var i = 0; i < length; i++) {
            password[i] = validChars[randomBytes[i] % validChars.Length];
        }

        return new string(password);
    }
}
