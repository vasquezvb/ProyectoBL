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
	/// Lógica de interacción para MenuClientes.xaml
	/// </summary>
	public partial class MenuClientes : MetroWindow
	{
		BeLifeContext Contexto { get; }

		public MenuClientes(BeLifeContext contexto)
		{
			InitializeComponent();
			Contexto = contexto;
		}

		private void W_Closed(Object sender, EventArgs e) => Visibility = Visibility.Visible;

		private void BtnAgregarCliente_Click(Object sender, RoutedEventArgs e)
		{
			var w = new AgregarCliente(Contexto);
			Visibility = Visibility.Hidden;
			w.Closed += W_Closed;
			w.ShowDialog();
		}

		private void BtnActualizarCliente_Click(Object sender, RoutedEventArgs e)
		{
			var w = new ActualizarCliente(Contexto);
			Visibility = Visibility.Hidden;
			w.Closed += W_Closed;
			w.ShowDialog();
		}

		private void BtnBorrarCliente_Click(Object sender, RoutedEventArgs e)
		{
			var w = new EliminarCliente(Contexto);
			Visibility = Visibility.Hidden;
			w.Closed += W_Closed;
			w.ShowDialog();
		}

		private void BtnListarClientes_Click(Object sender, RoutedEventArgs e)
		{
			var w = new ListarClientes(Contexto);
			Visibility = Visibility.Hidden;
			w.Closed += W_Closed;
			w.ShowDialog();
		}
	}
}
