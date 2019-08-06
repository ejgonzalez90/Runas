using Runas.Service;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Runas.Presentation.Programs
{
    /// <summary>
    /// Lógica de interacción para Index.xaml
    /// </summary>
    public partial class Index : Page
    {
        IProgramService programService;

        public Index(IProgramService programService)
        {
            this.programService = programService;
        }

        public Index()
        {
            // TODO: DI
            this.programService = new ProgramService();

            InitializeComponent();
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            string program = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE\\devenv.exe";
            string arguments = Arguments.Text;
            string userName = (string)((ComboBoxItem)User.SelectedValue).Content;
            string password = string.Empty;

            programService.Runas(
                program: program,
                arguments: arguments,
                userName: userName,
                password: password
                );
        }

        private void Execute_Credentials(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
