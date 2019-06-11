using Runas.Security;
using Runas.Security.Interfaces;
using Runas.Service;
using System;
using System.Collections.Generic;
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
            string program = Application.SelectedValue.ToString();
            string user = User.SelectedValue.ToString();

            programService.Runas(
                program                
                );
        }
    }
}
