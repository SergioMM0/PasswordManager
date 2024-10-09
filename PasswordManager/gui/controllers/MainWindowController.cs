using System.Collections.ObjectModel;
using PasswordManager.be;
using PasswordManager.dal;

namespace PasswordManager.gui.controllers;

public class MainWindowController {
    private readonly AccountDAO _accountDAO;

    public ObservableCollection<Account> Accounts { get; set; }

    public MainWindowController() {
        _accountDAO = new AccountDAO();
        LoadAccounts();
    }

    private void LoadAccounts() {
        var accountsFromDb = _accountDAO.GetAllAccounts();
        Accounts = new ObservableCollection<Account>(accountsFromDb);
    }

    public void AddAccount(Account newAccount) {
        Accounts.Add(newAccount);
    }

    public void UpdateAccount(Account existingAccount, Account updatedAccount) {
        int index = Accounts.IndexOf(existingAccount);
        if (index >= 0) {
            Accounts[index] = updatedAccount;
        }
    }

    public void DeleteAccount(Account account) {
        Accounts.Remove(account);
    }
}
