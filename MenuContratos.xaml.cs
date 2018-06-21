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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
namespace BeLifeRe
{
	/// <summary>
	/// Lógica de interacción para MenuContratos.xaml
	/// </summary>
	public partial class MenuContratos : MetroWindow
    {
		BeLifeContext Contexto { get; }
        public new Visibility Visibility { get; private set; }

        public MenuContratos(BeLifeContext contexto)
		{
			InitializeComponent();
			Contexto = contexto;
		}

		private void W_Closed(Object sender, EventArgs e) => Visibility = Visibility.Visible;

		private void BtnAgregarContrato_Click(Object sender, RoutedEventArgs e)
		{
			var w = new AgregarContrato(Contexto);
			Visibility = Visibility.Hidden;
			w.Closed += W_Closed;
			w.ShowDialog();
		}

		private void BtnActualizarContrato_Click(Object sender, RoutedEventArgs e)
		{
			var w = new ActualizarContrato(Contexto);
			Visibility = Visibility.Hidden;
			w.Closed += W_Closed;
			w.ShowDialog();
		}

		private void BtnAnularContrato_Click(Object sender, RoutedEventArgs e)
		{
			var w = new AnularContrato(Contexto);
			Visibility = Visibility.Hidden;
			w.Closed += W_Closed;
			w.ShowDialog();
		}

		private void BtnListarContratos_Click(Object sender, RoutedEventArgs e)
		{
			var w = new ListarContratos(Contexto);
			Visibility = Visibility.Hidden;
			w.Closed += W_Closed;
			w.ShowDialog();
		}
	}
}
