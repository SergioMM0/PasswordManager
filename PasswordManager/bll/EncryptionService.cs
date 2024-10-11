using System.IO;
using System.Security.Cryptography;

namespace PasswordManager.bll {
    public class EncryptionService {
        private const int KeySize = 256; // Key size for AES (256 bits = 32 bytes)
        private const int SaltSize = 16; // Salt size (128 bits = 16 bytes)
        private const int Iterations = 10000; // Number of iterations for PBKDF2
        private readonly string _password;

        public EncryptionService(string password) {
            _password = password;
        }

        public string Encrypt(string plainText) {
            // Generate a new salt for this encryption
            byte[] salt = GenerateSalt();

            // Derive the key from the password and salt
            var key = DeriveKeyFromPassword(_password, salt);

            // Create an AES object
            using (Aes aes = Aes.Create()) {
                aes.Key = key;
                aes.GenerateIV(); // Generate a random initialization vector (IV)
                byte[] iv = aes.IV;

                using (var encryptor = aes.CreateEncryptor(aes.Key, iv))
                using (var ms = new MemoryStream()) {
                    ms.Write(salt, 0, salt.Length); // Write the salt to the beginning of the stream
                    ms.Write(iv, 0, iv.Length); // Write the IV to the stream

                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs)) {
                        sw.Write(plainText); // Write the plain text to be encrypted
                    }

                    // Return the encrypted data as a Base64 string
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Decrypt(string encryptedText) {
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);

            using (var ms = new MemoryStream(cipherBytes)) {
                // Read the salt from the stream
                byte[] salt = new byte[SaltSize];
                ms.Read(salt, 0, salt.Length);

                // Derive the key from the password and salt
                var key = DeriveKeyFromPassword(_password, salt);

                // Read the IV from the stream
                byte[] iv = new byte[16];
                ms.Read(iv, 0, iv.Length);

                using (Aes aes = Aes.Create()) {
                    aes.Key = key;
                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs)) {
                        // Return the decrypted data as plain text
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        private byte[] DeriveKeyFromPassword(string password, byte[] salt) {
            using var keyDerivation = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            return keyDerivation.GetBytes(KeySize / 8);
        }

        private byte[] GenerateSalt() {
            byte[] salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);
            return salt;
        }
    }
}
