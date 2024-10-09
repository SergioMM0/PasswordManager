using System.Windows;
using PasswordManager.be;
using PasswordManager.gui.controllers;

namespace PasswordManager.gui;

public partial class MainWindow : Window {
    private MainWindowController _controller;

    public MainWindow() {
        InitializeComponent();
        _controller = new MainWindowController();
        DataContext = _controller;
        AccountDataGrid.ItemsSource = _controller.Accounts;
    }

    private void AddButton_Click(object sender, RoutedEventArgs e) {

        var dialog = new AddAccountDialog();
        if (dialog.ShowDialog() == true) {
            var newAccount = dialog.NewAccount;
            _controller.AddAccount(newAccount);
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
            _controller.DeleteAccount(selectedAccount);
        }
    }
}
