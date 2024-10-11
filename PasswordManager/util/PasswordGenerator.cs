using System.Security.Cryptography;

namespace PasswordManager.util;

public static class PasswordGenerator {
    public static string GenerateSecurePassword(int length) {
        const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
        char[] password = new char[length];
        byte[] randomBytes = new byte[length];

        using (var rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(randomBytes);
        }

        for (int i = 0; i < length; i++) {
            password[i] = validChars[randomBytes[i] % validChars.Length];
        }

        return new string(password);
    }
}
