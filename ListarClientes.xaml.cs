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
    /// Lógica de interacción para BusquedaFiltro.xaml
    /// </summary>
    public partial class ListarClientes : MetroWindow
    {
		BeLifeContext Contexto { get; }

		public ListarClientes(BeLifeContext contexto)
		{
			InitializeComponent();
			Contexto = contexto;
			DtgClientes.ItemsSource = Contexto.Cliente.ToList();
			CbEstadoCivil.ItemsSource = Contexto.EstadoCivil.ToList();
        }

		private void BtnFiltrar_Click(Object sender, RoutedEventArgs e)
		{
			var filtro = Contexto.Cliente.ToList();

			if (ChkRut.IsChecked.Value)
			{
				if (!Contexto.IsRutValido(TbRut.Text))
				{
					MessageBox.Show("Ingrese un rut válido");
					return;
				}
				filtro = filtro.Where(c => c.RutCliente.Equals(TbRut.Text)).ToList();
			}

			if(ChkEstadoCivil.IsChecked.Value)
			{
				if (CbEstadoCivil.SelectedItem as EstadoCivil == null)
				{
					MessageBox.Show("Seleccione un estado civil");
					return;
				}
				filtro = filtro.Where(c => c.EstadoCivil.Equals(CbEstadoCivil.SelectedItem as EstadoCivil)).ToList();
			}

			if(ChkSexo.IsChecked.Value) { filtro = filtro.Where(c => c.Sexo.Descripcion.Equals(RbMasculino.IsChecked.Value ? "Hombre" : "Mujer")).ToList(); }

			DtgClientes.ItemsSource = filtro;
		}

		private void BtnActualizar_Click(Object sender, RoutedEventArgs e)
		{
			if (!(DtgClientes.SelectedItem is Cliente cliente))
			{
				MessageBox.Show("Seleccione un contrato");
				return;
			}
			new ActualizarCliente(Contexto, cliente).ShowDialog();
			BtnFiltrar_Click(this, null);
		}

		private void BtnEliminar_Click(Object sender, RoutedEventArgs e)
		{
			var cliente = DtgClientes.SelectedItem as Cliente;
			if (cliente == null)
			{
				MessageBox.Show("Seleccione un cliente");
				return;
			}
			new EliminarCliente(Contexto, cliente);
			BtnFiltrar_Click(this, null);
		}
	}
}
