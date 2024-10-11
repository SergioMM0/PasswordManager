using System.Windows;
using PasswordManager.dal; // Import the DAO
using System.Security.Cryptography;
using PasswordManager.util;

namespace PasswordManager.gui {
    public partial class MasterKey : Window {
        private readonly MasterKeyDAO _masterKeyDao;

        public MasterKey() {
            InitializeComponent();
            _masterKeyDao = new MasterKeyDAO();

            if (!_masterKeyDao.MasterKeyExists()) {
                MessageBox.Show(
                    "This is the first time you're setting a password. The password you enter now will be used for future logins.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        // Enter button event handler
        private void EnterButton_Click(object sender, RoutedEventArgs e) {
            
            var enteredPassword = PasswordTextBox.Password;

            // Check if this is the first time setting the password
            if (!_masterKeyDao.MasterKeyExists()) {
                // Hash and salt the entered password
                var (hashedPassword, salt) = TheTruePasswordManager.HashPassword(enteredPassword);

                // Save the hashed password and salt to the database
                _masterKeyDao.InsertMasterKey(hashedPassword, salt);

                // Optionally, proceed to MainWindow here
                OpenMainWindow(enteredPassword);
            }
            else {
                // Retrieve the hashed password and salt from the database
                var masterKeyData = _masterKeyDao.GetMasterKey();

                if (masterKeyData != null) {
                    string storedHashedPassword = masterKeyData.Value.HashedKey;
                    string storedSalt = masterKeyData.Value.Salt;

                    // Verify the entered password with the stored hashed password
                    if (TheTruePasswordManager.VerifyPassword(enteredPassword, storedHashedPassword, storedSalt)) {
                        OpenMainWindow(enteredPassword); // Open MainWindow if password is correct
                    }
                    else {
                        MessageBox.Show("Incorrect Password. The application will now close.");
                        Application.Current.Shutdown();
                    }
                }
            }
        }

        private void OpenMainWindow(string password) {
            MainWindow mainWindow = new MainWindow(password);
            mainWindow.Show();
            Close();
        }
    }
}
