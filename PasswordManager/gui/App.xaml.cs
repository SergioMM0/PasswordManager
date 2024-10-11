using System.Windows;
using PasswordManager.dal;

namespace PasswordManager.gui {
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            var databaseInitializer = new DatabaseInitializer();
        }
    }
}
