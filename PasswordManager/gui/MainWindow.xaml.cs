using System.Windows;
using PasswordManager.be;
using PasswordManager.gui.controllers;

namespace PasswordManager.gui;

public partial class MainWindow : Window {
    private MainWindowController _windowController;

    public MainWindow() {
        InitializeComponent();
        _windowController = new MainWindowController();
        AccountDataGrid.ItemsSource = _windowController.Accounts;
    }

    private void AddButton_Click(object sender, RoutedEventArgs e) {
        var newAccount = new Account { Provider = "Twitter", Username = "newuser", Password = "newpass" };
        _windowController.AddAccount(newAccount);
    }

    private void EditButton_Click(object sender, RoutedEventArgs e) {
        if (AccountDataGrid.SelectedItem is Account selectedAccount) {
            selectedAccount.Password = "UpdatedPassword";
            AccountDataGrid.Items.Refresh();
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e) {
        if (AccountDataGrid.SelectedItem is Account selectedAccount) {
            _windowController.DeleteAccount(selectedAccount);
        }
    }
}
