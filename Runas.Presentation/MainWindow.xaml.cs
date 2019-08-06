using Runas.Service;
using System.ComponentModel;
using System.Windows.Navigation;

namespace Runas.Presentation
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        IPasswordService passwordService;
        IProgramService programService;

        public MainWindow()
        {
            InitializeComponent();

            var userName = "ejgonzalez90@hotmail.com";

            this.passwordService = PasswordService.GetInstance("password");
            var password = passwordService.GetUserPassword(userName);

            this.programService = new ProgramService();
            //programService.Runas("cmd", string.Empty, userName, password);

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            passwordService.Dispose();

            base.OnClosing(e);
        }
    }
}
