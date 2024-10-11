using System.Windows;
using PasswordManager.be;
using PasswordManager.gui.models;

namespace PasswordManager.gui;

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

    private void EditButton_Click(object sender, RoutedEventArgs e) {
        if (AccountDataGrid.SelectedItem is Account selectedAccount) {
            selectedAccount.Password = "UpdatedPassword";
            AccountDataGrid.Items.Refresh();
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e) {
        if (AccountDataGrid.SelectedItem is Account selectedAccount) {
            _model.DeleteAccount(selectedAccount);
        }
    }
}
