using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL;
using MahApps.Metro.Controls;

namespace BeLifeRe
{
    /// <summary>
    /// Lógica de interacción para Clientes.xaml
    /// </summary>
    public partial class AgregarCliente : MetroWindow
    {
		BeLifeContext Contexto { get; }

		public AgregarCliente(BeLifeContext contexto)
		{
			InitializeComponent();
			Contexto = contexto;

            /* Cargar estados civiles */
            CbEstadoCivil.ItemsSource = Contexto.EstadoCivil.ToList();
			/* Fecha nacimiento mínima */
			var nowMinus18 = DateTime.Today.AddYears(-18);
            DtpFechaNacimiento.DisplayDateEnd = nowMinus18;
		}

		private void BtnAgregar_Click(Object sender, RoutedEventArgs e)
		{

            if (!Contexto.IsRutValido(TbRut.Text, false))
            {
                MessageBox.Show("Ingrese un rut válido");
                return;
            }

            if (Contexto.IsRutValido(TbRut.Text))
            {
                MessageBox.Show("El rut ingresado ya existe en la base de datos");
                return;
            }

            if (TbNombres.Text == null || TbNombres.Text.Equals(String.Empty) || Regex.IsMatch(TbNombres.Text, @"\d"))
            {
                MessageBox.Show("Ingrese un nombre válido");
                return;
            }

            if (TbApellidos.Text == null || TbApellidos.Text.Equals(String.Empty) || Regex.IsMatch(TbApellidos.Text, @"\d"))
            {
                MessageBox.Show("Ingrese un apellido válido");
                return;
            }

            if (!(CbEstadoCivil.SelectedItem is EstadoCivil))
            {
                MessageBox.Show("Seleccione un estado civil");
                return;
            }

            ClienteBLL clienteBLL = new ClienteBLL();
           
                clienteBLL.Agregar(TbRut.Text,
                TbNombres.Text,
                TbApellidos.Text,
                DtpFechaNacimiento.DisplayDate,
                Contexto.Sexo.FirstOrDefault(x => x.Descripcion.Equals(RbMasculino.IsChecked.Value ? "Hombre" : "Mujer")).IdSexo,
                (CbEstadoCivil.SelectedItem as EstadoCivil).IdEstadoCivil
                );

            
			var cliente = new Cliente()
			{
				RutCliente = TbRut.Text,
				Nombres = TbNombres.Text,
				Apellidos = TbApellidos.Text,
				FechaNacimiento = DtpFechaNacimiento.DisplayDate,
				Sexo = Contexto.Sexo.FirstOrDefault(x => x.Descripcion.Equals(RbMasculino.IsChecked.Value ? "Hombre" : "Mujer")),
				EstadoCivil = CbEstadoCivil.SelectedItem as EstadoCivil
            };	

			MessageBox.Show(
$@"Creado cliente:
  Rut : {cliente.RutCliente}
  Nombre completo : {cliente.Apellidos}, {cliente.Nombres} 
  Fecha de nacimiento : {cliente.FechaNacimiento:yyyy/MM/dd}
  Sexo : {cliente.Sexo.Descripcion}
  Estado civil : {cliente.EstadoCivil}"
);
        }
	}
}
