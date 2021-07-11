using FleetManApiController.Services;
using SimpleWPFLogger.Enums;
using SimpleWPFLogger.Model;
using SimpleWPFLogger.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace FleetManApiController
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        LoggerManager LoggerManager = null;

        public MainWindow()
        {
            InitializeComponent();

            LoggerManager = LoggerManager.GetInstance();

            LoggerManager.AddLogger("MainLogger", this.Logger);

            Logger.PrintText(new Run("Iniciando aplicação..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));

            CheckDbConnection();

            ServiceStatusUC.ChangeStatus(false);
        }

        private async void StartService()
        {
            try
            {
                Logger.PrintText(new Run("Iniciando serviço da API Web..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));
                bool status = OwinService.Start();
                await ServiceStatusUC.ChangeStatus(status);
                Logger.PrintText(new Run("Serviço da API Web inicializado..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro ao iniciar serviço", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.PrintText(new Run("Erro na inicialização serviço da API Web..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));
            }
        }

        private async void StopService(bool logerror = true)
        {
            try
            {
                Logger.PrintText(new Run("Finalizando serviço..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));
                bool status = OwinService.Stop();
                await ServiceStatusUC.ChangeStatus(!status);
            }
            catch (Exception ex)
            {
                if (logerror)
                {
                    MessageBox.Show(ex.ToString(), "Erro ao finalizar serviço", MessageBoxButton.OK, MessageBoxImage.Error);
                    Logger.PrintText(new Run("Erro na finalização do serviço..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));
                }
            }
        }

        private async Task CheckDbConnection()
        {
            Logger.PrintText(new Run("Checando conexão com o banco..."), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD));
            bool connection = ConnectionManager.CheckDbConnection();
            await DBConnectionStatusUC.ChangeStatus(connection);
        }

        private void StartServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            StartService();
        }

        private void StopService_Event(object sender, RoutedEventArgs e)
        {
            StopService();
        }

        private async void CheckDbConnection_Event(object sender, EventArgs e)
        {
            await CheckDbConnection();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopService(false);
        }

        private async void RefreshDbConnection_Click(object sender, RoutedEventArgs e)
        {
            await CheckDbConnection();
        }
    }
}
