using System.Windows;
using PasswordManager.dal; // Import the DAO
using System.Security.Cryptography;

namespace PasswordManager.gui
{
    public partial class MasterKey : Window
    {
        private readonly MasterKeyDAO _masterKeyDao;

        public MasterKey()
        {
            InitializeComponent();
            _masterKeyDao = new MasterKeyDAO(); // Initialize DAO

            // Check if the MasterKey exists in the database
            if (!_masterKeyDao.MasterKeyExists())
            {
                MessageBox.Show("This is the first time you're setting a password. The password you enter now will be used for future logins.");
            }
        }

        // Cancel button event handler to close the window and exit the application
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            Application.Current.Shutdown();
        }

        // Enter button event handler
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            var enteredPassword = PasswordTextBox.Password;

            // Check if this is the first time setting the password
            if (!_masterKeyDao.MasterKeyExists())
            {
                // Hash and salt the entered password
                var (hashedPassword, salt) = HashPassword(enteredPassword);

                // Save the hashed password and salt to the database
                _masterKeyDao.InsertMasterKey(hashedPassword, salt);
                
                // Optionally, proceed to MainWindow here
                OpenMainWindow(enteredPassword);
            }
            else
            {
                // Retrieve the hashed password and salt from the database
                var masterKeyData = _masterKeyDao.GetMasterKey();

                if (masterKeyData != null)
                {
                    string storedHashedPassword = masterKeyData.Value.HashedKey;
                    string storedSalt = masterKeyData.Value.Salt;

                    // Verify the entered password with the stored hashed password
                    if (VerifyPassword(enteredPassword, storedHashedPassword, storedSalt))
                    {
                        OpenMainWindow(enteredPassword); // Open MainWindow if password is correct
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Password. The application will now close.");
                        Application.Current.Shutdown();
                    }
                }
            }
        }
        
        private void OpenMainWindow(string password)
        {
            MainWindow mainWindow = new MainWindow(password);
            mainWindow.Show();
            Close();
        }
        
        private (string HashedPassword, string Salt) HashPassword(string password)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes); // Generate a random salt
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); // Derive a 256-bit key

                string hashedPassword = Convert.ToBase64String(hashBytes);
                string salt = Convert.ToBase64String(saltBytes);

                return (hashedPassword, salt);
            }
        }
        
        private bool VerifyPassword(string enteredPassword, string storedHashedPassword, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); // Derive a 256-bit key from entered password

                string enteredHashedPassword = Convert.ToBase64String(hashBytes);

                // Compare the stored hashed password with the one generated from the entered password
                return enteredHashedPassword == storedHashedPassword;
            }
        }
    }
}
