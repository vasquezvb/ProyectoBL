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
using MahApps.Metro.Controls;
namespace BeLifeRe
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
		BeLifeContext Contexto { get; } = new BeLifeContext();

        public MainWindow()
        {
            InitializeComponent();
        }

		private void BtnClientes_Click(Object sender, RoutedEventArgs e)
		{
			var w = new MenuClientes(Contexto);
			Visibility = Visibility.Hidden;
			w.Closed += W_Closed;
			w.ShowDialog();
		}

		private void BtnContratos_Click(Object sender, RoutedEventArgs e)
		{
			var w = new MenuContratos(Contexto);
			Visibility = Visibility.Hidden;
			w.Closed += W_Closed;
			w.ShowDialog();
		}

		private void W_Closed(Object sender, EventArgs e) => Visibility = Visibility.Visible;
	}
}
