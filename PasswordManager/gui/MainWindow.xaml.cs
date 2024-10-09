using System.Windows;
using PasswordManager.be;
using PasswordManager.gui.models;

namespace PasswordManager.gui;

public partial class MainWindow : Window {
    private MainWindowModel _windowModel;

    public MainWindow() {
        InitializeComponent();
        _windowModel = new MainWindowModel();
        AccountDataGrid.ItemsSource = _windowModel.Accounts;
    }

    private void AddButton_Click(object sender, RoutedEventArgs e) {
        var newAccount = new Account { Provider = "Twitter", Username = "newuser", Password = "newpass" };
        _windowModel.AddAccount(newAccount);
    }

    private void EditButton_Click(object sender, RoutedEventArgs e) {
        if (AccountDataGrid.SelectedItem is Account selectedAccount) {
            selectedAccount.Password = "UpdatedPassword";
            AccountDataGrid.Items.Refresh();
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e) {
        if (AccountDataGrid.SelectedItem is Account selectedAccount) {
            _windowModel.DeleteAccount(selectedAccount);
        }
    }
}
