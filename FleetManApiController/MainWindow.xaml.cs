using FleetManApiController.Services;
using SimpleWPFLogger.Enums;
using SimpleWPFLogger.Model;
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

namespace FleetManApiController
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Logger.PrintText(new Run("Iniciando aplicação..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));
            UpdateDBConnection();
        }

        private async void UpdateDBConnection()
        {
            Logger.PrintText(new Run("Checando conexão com o banco..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));
            bool connection = ConnectionManager.CheckDbConnection();
            await DBConnectionStatusUC.ChangeStatus(connection);
        }
    }
}
