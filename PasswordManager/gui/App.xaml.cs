using System.Windows;
using PasswordManager.dal;

namespace PasswordManager.gui {
    public partial class App : Application {
        private DatabaseManager _databaseManager;

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            _databaseManager = new DatabaseManager();
        }
    }
}
