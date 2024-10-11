using System.Windows;
using PasswordManager.be;
using PasswordManager.gui.models;
using PasswordManager.util;

namespace PasswordManager.gui {
    public partial class MainWindow : Window {
        private MainWindowModel _model;

        public MainWindow(string password) {
            InitializeComponent();
            _model = new MainWindowModel(password);
            DataContext = _model;
            AccountDataGrid.ItemsSource = _model.Accounts;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            var dialog = new AddAccountDialog();
            if (dialog.ShowDialog() == true) {
                var newAccount = dialog.NewAccount;
                _model.AddAccount(newAccount);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) {
            if (AccountDataGrid.SelectedItem is Account selectedAccount) {
                var result = MessageBox.Show("Are you sure you want to update the password for this account?",
                    "Confirm Password Update",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes) {
                    var newPassword = TheTruePasswordManager.GenerateSecurePassword(16);
                    selectedAccount.Password = newPassword;
                    _model.UpdateAccount(selectedAccount);
                    AccountDataGrid.Items.Refresh();
                    MessageBox.Show("Password updated successfully.");
                }
            }
            else {
                MessageBox.Show("Please select an account.");
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            if (AccountDataGrid.SelectedItem is Account selectedAccount) {
                _model.DeleteAccount(selectedAccount);
            }
        }

        private void CopyPasswordButton_Click(object sender, RoutedEventArgs e) {
            if (AccountDataGrid.SelectedItem is Account selectedAccount) {
                Clipboard.SetText(selectedAccount.Password);
            }
            else {
                MessageBox.Show("Please select an account.");
            }
        }
    }
}
