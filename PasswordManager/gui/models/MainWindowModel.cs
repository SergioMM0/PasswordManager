using System.Collections.ObjectModel;
using PasswordManager.be;
using PasswordManager.dal;

namespace PasswordManager.gui.models;

public class MainWindowModel {
    private readonly AccountDAO _accountDAO;

    public ObservableCollection<Account> Accounts { get; set; }

    public MainWindowModel(string password) {
        _accountDAO = new AccountDAO(password);
        Accounts = new ObservableCollection<Account>();
        LoadAccounts();
    }

    private void LoadAccounts() {
        var accountsFromDb = _accountDAO.GetAllAccounts();
        foreach (var account in accountsFromDb) Accounts.Add(account);
    }

    public void AddAccount(Account newAccount) {
        var account = _accountDAO.InsertAccount(newAccount);
        Accounts.Add(account);
    }

    public void UpdateAccount(Account updatedAccount) {
        _accountDAO.UpdateAccount(updatedAccount);
    }

    public void DeleteAccount(Account account) {
        _accountDAO.DeleteAccount(account.Id);
        Accounts.Remove(account);
    }
}
