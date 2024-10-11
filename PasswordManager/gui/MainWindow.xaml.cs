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
                string newPassword = PasswordGenerator.GenerateSecurePassword(16);

                selectedAccount.Password = newPassword;

                // Call the model's update account method
                _model.UpdateAccount(selectedAccount);

                // Refresh the UI
                AccountDataGrid.Items.Refresh();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            if (AccountDataGrid.SelectedItem is Account selectedAccount) {
                _model.DeleteAccount(selectedAccount);
            }
        }
    }
}
