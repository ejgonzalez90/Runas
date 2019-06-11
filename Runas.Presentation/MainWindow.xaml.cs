using Runas.Security;
using Runas.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Runas.Presentation
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        IPasswordService passwordService;

        public MainWindow()
        {
            InitializeComponent();

            this.passwordService = PasswordService.GetInstance("password");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            passwordService.Dispose();

            base.OnClosing(e);
        }
    }
}
