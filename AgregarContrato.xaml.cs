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
using MahApps.Metro.Controls;
namespace BeLifeRe
{
    /// <summary>
    /// Lógica de interacción para Contratos.xaml
    /// </summary>
    public partial class AgregarContrato : MetroWindow
    {
		BeLifeContext Contexto { get; }

		public AgregarContrato(BeLifeContext contexto)
		{
			InitializeComponent();
			Contexto = contexto;

			CbPlan.ItemsSource = Contexto.Plan.ToList();

			DtpInicioVigencia.DisplayDateStart = DateTime.Now;
			DtpInicioVigencia.DisplayDate = DateTime.Now;
			DtpInicioVigencia.DisplayDateEnd = DateTime.Now + TimeSpan.FromDays(31);
		}

		private void RecalcularPrima(Double primaBase)
		{
			var cliente = Contexto.Cliente.FirstOrDefault(c => c.RutCliente == TbRut.Text);
			var edad = (DateTime.Now - cliente.FechaNacimiento).TotalDays / 365;
			var sexo = cliente.Sexo.Descripcion;
			var estadoCivil = cliente.EstadoCivil.Descripcion;
			var recargo = 0d;

			if(edad > 45) { recargo += 6; }
			else if(edad > 26) { recargo += 2.4d; }
			else { recargo += 3.6d; }

			if(sexo.Equals("Hombre")) { recargo += 2.4d; }
			else { recargo += 1.2d; }

			if(estadoCivil.Equals("Soltero")) { recargo += 4.8d; }
			else if(estadoCivil.Equals("Casado")) { recargo += 2.4d; }
			else { recargo += 3.6d; }

			var prima = primaBase + recargo;
			TbPrimaAnual.Text = prima.ToString("0.00");
			TbPrimaMensual.Text = (prima / 12d).ToString("0.00");
		}

		private void BtnAgregar_Click(Object sender, RoutedEventArgs e)
		{
			if(!Contexto.IsRutValido(TbRut.Text))
			{
				MessageBox.Show("Ingrese un rut válido");
				return;
			}
			if(CbPlan.SelectedItem as Plan == null)
			{
				MessageBox.Show("Seleccione un plan");
				return;
			}

			var dtContrato = DateTime.Now;
			var contrato = new Contrato
			{
				Cliente = Contexto.Cliente.FirstOrDefault(c => c.RutCliente.Equals(TbRut.Text)),
				Numero = dtContrato.ToString("yyyyMMddHHmmss"),
				DeclaracionSalud = ChkSalud.IsChecked.Value,
				FechaCreacion = dtContrato,
				FechaInicioVigencia = DtpInicioVigencia.DisplayDate,
				FechaFinVigencia = DtpInicioVigencia.DisplayDate + TimeSpan.FromDays(365),
				Observaciones = TbObservaciones.Text.Equals(String.Empty) ? "<Vacio>" : TbObservaciones.Text,
				Plan = CbPlan.SelectedItem as Plan,
				PrimaAnual = Double.Parse(TbPrimaAnual.Text),
				PrimaMensual = Double.Parse(TbPrimaAnual.Text) / 12d,
				Vigente = true
			};

			Contexto.Contrato.Add(contrato);
			Contexto.SaveChanges();
			var list = Contexto.Contrato.ToList();

			MessageBox.Show(
				"Contrato creado:\n" +
				$"  N° Contrato\t:\t{contrato.Numero}\n" +
				$"  Rut cliente\t:\t{contrato.Cliente.RutCliente}\n" +
				$"  Fecha inicio\t:\t{contrato.FechaInicioVigencia}\n" +
				$"  Fecha término\t:\t{contrato.FechaFinVigencia}\n" +
				$"  Plan asociado\t:\t{contrato.Plan}\n" +
				$"  Prima anual\t:\t{contrato.PrimaAnual}\n" +
				$"  Prima mensual\t:\t{contrato.PrimaMensual}\n"
				, "Contrato creado con éxito", MessageBoxButton.OK, MessageBoxImage.Information);
			Close();
		}

		private void CbPlan_SelectionChanged(Object sender, SelectionChangedEventArgs e)
		{
			if(Contexto.IsRutValido(TbRut.Text)){ RecalcularPrima(((sender as ComboBox).SelectedItem as Plan).PrimaBase); }
		}


		private void TbRut_TextChanged(Object sender, TextChangedEventArgs e)
		{
			var plan = CbPlan.SelectedItem as Plan;
			if (Contexto.IsRutValido(TbRut.Text) && plan != null) { RecalcularPrima(plan.PrimaBase); }
		}
	}
}
