using System.Windows;
using PasswordManager.be;

namespace PasswordManager.gui {
    public partial class AddAccountDialog : Window {
        public Account NewAccount { get; private set; }

        public AddAccountDialog() {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) {
            if (!string.IsNullOrWhiteSpace(ProviderForm.Text) &&
                !string.IsNullOrWhiteSpace(UsernameForm.Text) &&
                !string.IsNullOrWhiteSpace(PasswordForm.Password)) {
                
                NewAccount = new Account {
                    Provider = ProviderForm.Text,
                    Username = UsernameForm.Text,
                    Password = PasswordForm.Password
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
