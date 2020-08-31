using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace FleetManApiController.UserControls
{
    /// <summary>
    /// Interação lógica para StatusLabelUC.xam
    /// </summary>
    public partial class StatusLabelUC : UserControl, INotifyPropertyChanged
    {
        private Color _greencolor = Color.FromArgb(255, 55, 255, 35);
        private Color _redcolor = Color.FromArgb(255, 255, 30, 30);

        public string Description { get; set; }

        public StatusLabelUC()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task ChangeStatus(bool active)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                if (active)
                {
                    StateLabel.Content = "Ativo";
                    StatusEllipse.Fill = new SolidColorBrush(_greencolor);
                }
                else
                {
                    StateLabel.Content = "Inativo";
                    StatusEllipse.Fill = new SolidColorBrush(_redcolor);
                }
            });
        }

        private void NotifyPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
