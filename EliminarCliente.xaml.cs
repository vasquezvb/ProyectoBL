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
    /// Lógica de interacción para EliminarCliente.xaml
    /// </summary>
    public partial class EliminarCliente : MetroWindow
    {
		BeLifeContext Contexto { get; }

		public EliminarCliente(BeLifeContext contexto)
        {
            InitializeComponent();
			Contexto = contexto;
        }

		public EliminarCliente(BeLifeContext contexto, Cliente Cliente) : this(contexto)
		{
			TbRut.Text = Cliente.RutCliente;
			BtnEliminarCliente_Click(this, null);
		}

		private void BtnEliminarCliente_Click(Object sender, RoutedEventArgs e)
		{
			var cliente = Contexto.Cliente.FirstOrDefault(c => c.RutCliente == TbRut.Text);
			if (TbRut.Text.Equals(String.Empty) || cliente == null)
			{
				MessageBox.Show("Rut de cliente inválido");
				return;
			}

			if (cliente.Contrato.Count > 0)
			{
				MessageBox.Show("El cliente posee contratos asociados, no puede ser eliminado del sistema");
				return;
			}

			var result = MessageBox.Show(
$@"Cliente:
  Rut: {cliente.RutCliente}
  Nombre: {cliente.Apellidos}, {cliente.Nombres}
¿Está seguro que desea eliminarlo?", "Confirmar eliminación de cliente", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.No) { return; }

			Contexto.Cliente.Remove(cliente);
			Contexto.SaveChanges();
			MessageBox.Show($"El cliente {cliente.RutCliente} ha sido eliminado", String.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
			Close();
		}
	}
}
