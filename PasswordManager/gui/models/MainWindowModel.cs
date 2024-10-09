using System.Collections.ObjectModel;
using PasswordManager.be;

namespace PasswordManager.gui.models;

public class MainWindowModel {
    public ObservableCollection<Account> Accounts { get; set; }

    public MainWindowModel() {
        // Initialize with sample data
        Accounts = new ObservableCollection<Account> {
            new Account { Provider = "Email", Username = "user@example.com", Password = "password123" },
            new Account { Provider = "Bank", Username = "user2", Password = "securepassword" }
        };
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