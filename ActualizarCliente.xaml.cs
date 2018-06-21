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
	/// Lógica de interacción para ActualizarCliente.xaml
	/// </summary>
	public partial class ActualizarCliente : MetroWindow
	{
		BeLifeContext Contexto { get; }

		Cliente CurrentCliente { get; set; }

		public ActualizarCliente(BeLifeContext contexto)
		{
			InitializeComponent();
			Contexto = contexto;
			DtpFechaNacimiento.DisplayDateEnd = DateTime.Today.AddYears(-18);
			CbEstadoCivil.ItemsSource = Contexto.EstadoCivil.ToList();
		}

		public ActualizarCliente(BeLifeContext contexto, Cliente cliente) : this(contexto)
		{
			TbRut.IsEnabled = false;
			TbRut.Text = cliente.RutCliente;
			IsVisibleChanged += ActualizarContrato_IsVisibleChanged;
		}

		private void ActualizarContrato_IsVisibleChanged(Object sender, DependencyPropertyChangedEventArgs e) => BtnRut_Click(this, null);

		private void BtnRut_Click(Object sender, RoutedEventArgs e)
		{
			CurrentCliente = Contexto.Cliente.FirstOrDefault(c => c.RutCliente.Equals(TbRut.Text));
			if (CurrentCliente == null)
			{
				MessageBox.Show("Ingrese un rut válido");
				return;
			}

			TbNombres.Text = CurrentCliente.Nombres;
			TbApellidos.Text = CurrentCliente.Apellidos;
			DtpFechaNacimiento.DisplayDate = CurrentCliente.FechaNacimiento;
			(CurrentCliente.Sexo.Descripcion.Equals("Hombre") ? RbMasculino : RbFemenino).IsChecked = true;
			CbEstadoCivil.SelectedItem = CurrentCliente.EstadoCivil;
		}

		private void BtnClienteActualizar_Click(Object sender, RoutedEventArgs e)
		{
			if(String.IsNullOrEmpty(TbNombres.Text))
			{
				MessageBox.Show("Ingrese un nombre válido");
				return;
			}

			if (String.IsNullOrEmpty(TbNombres.Text))
			{
				MessageBox.Show("Ingrese un apellido válido");
				return;
			}

			if (!(CbEstadoCivil.SelectedItem is EstadoCivil estadoCivil))
			{
				MessageBox.Show("Seleccione un estado civil");
				return;
			}

			if(TbNombres.Text.Equals(CurrentCliente.Nombres)
				&& TbApellidos.Text.Equals(CurrentCliente.Apellidos)
				&& DtpFechaNacimiento.DisplayDate.Equals(CurrentCliente.FechaNacimiento)
				&& (RbMasculino.IsChecked.Value ? "Hombre" : "Mujer").Equals(CurrentCliente.Sexo.Descripcion)
				&& estadoCivil.Equals(CurrentCliente.EstadoCivil))
			{
				MessageBox.Show("No hay datos para modificar");
				return;
			}

			var result = MessageBox.Show(
$"Se actualizarán los siguientes datos del cliente {CurrentCliente.RutCliente}:" +
(TbNombres.Text.Equals(CurrentCliente.Nombres) ? 
	"" : $"\n  Nombres : {CurrentCliente.Nombres} -> {TbNombres.Text}") +
(TbApellidos.Text.Equals(CurrentCliente.Apellidos) ? 
	"" : $"\n  Apellidos : {CurrentCliente.Apellidos} -> {TbApellidos.Text}") +
(DtpFechaNacimiento.DisplayDate.Equals(CurrentCliente.FechaNacimiento) ? 
	"" : $"\n  Fecha nacimiento : {CurrentCliente.FechaNacimiento:yyyy/MM/d} -> {TbNombres.Text:yyyy/MM/dd}") +
((RbMasculino.IsChecked.Value ? "Hombre" : "Mujer").Equals(CurrentCliente.Sexo.Descripcion) ? 
	"" : ($"\n  Sexo : {CurrentCliente.Sexo} -> " + (RbMasculino.IsChecked.Value ? "Hombre" : "Mujer"))) + 
(estadoCivil.Equals(CurrentCliente.EstadoCivil) ? 
	"" : $"\n  Estado civil : {CurrentCliente.EstadoCivil} -> {estadoCivil}"), 
				"Confirmar actualización de datos", 
				MessageBoxButton.OKCancel);

			if (result == MessageBoxResult.OK)
			{
				CurrentCliente.Nombres = TbNombres.Text;
				CurrentCliente.Apellidos = TbApellidos.Text;
				CurrentCliente.FechaNacimiento = DtpFechaNacimiento.DisplayDate;
				CurrentCliente.Sexo = Contexto.Sexo.FirstOrDefault(x => x.Descripcion.Equals(((RbMasculino.IsChecked.Value ? "Hombre" : "Mujer"))));
				CurrentCliente.EstadoCivil = estadoCivil;
				Contexto.SaveChanges();
			}

			Close();
		}
	}
}
