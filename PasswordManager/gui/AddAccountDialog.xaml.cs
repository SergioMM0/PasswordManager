using System.Windows;
using PasswordManager.be;

namespace PasswordManager.gui {
    public partial class AddAccountDialog : Window {
        public Account NewAccount { get; private set; }

        public AddAccountDialog() {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) {
            if (!string.IsNullOrWhiteSpace(ProviderTextBox.Text) &&
                !string.IsNullOrWhiteSpace(UsernameTextBox.Text) &&
                !string.IsNullOrWhiteSpace(PasswordBox.Password)) {
                
                NewAccount = new Account {
                    Provider = ProviderTextBox.Text,
                    Username = UsernameTextBox.Text,
                    Password = PasswordBox.Password
                };

                DialogResult = true;
            } else {
                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
