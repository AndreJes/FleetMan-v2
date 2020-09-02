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
            ServiceStatusUC.ChangeStatus(false);
        }

        private async void UpdateDBConnection()
        {
            Logger.PrintText(new Run("Checando conexão com o banco..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));
            bool connection = ConnectionManager.CheckDbConnection();
            await DBConnectionStatusUC.ChangeStatus(connection);
        }

        private async void StartService()
        {
            bool status;
            try
            {
                Logger.PrintText(new Run("iniciando serviço..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));
                status = OwinService.Start();
            }
            catch (Exception ex)
            {
                status = false;
                MessageBox.Show(ex.ToString(), "Erro ao iniciar serviço", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.PrintText(new Run("Erro na inicialização do serviço..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));
            }
            await ServiceStatusUC.ChangeStatus(status);
        }

        private async void StopService(bool logerror)
        {
            bool status;
            try
            {
                Logger.PrintText(new Run("Finalizando serviço..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));
                status = OwinService.Stop();
            }
            catch (Exception ex)
            {
                status = false;
                if (logerror)
                {
                    MessageBox.Show(ex.ToString(), "Erro ao finalizar serviço", MessageBoxButton.OK, MessageBoxImage.Error);
                    Logger.PrintText(new Run("Erro na finalização do serviço..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));
                }
            }
            await ServiceStatusUC.ChangeStatus(!status);
        }

        private void StartServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            StartService();
        }

        private void StopService_Event(object sender, RoutedEventArgs e)
        {
            StopService(true);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopService(false);
        }
    }
}
