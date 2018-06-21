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
    /// Lógica de interacción para ListarContratos.xaml
    /// </summary>
    public partial class ListarContratos : MetroWindow
    {
		BeLifeContext Contexto { get; }

        public ListarContratos(BeLifeContext contexto)
        {
            InitializeComponent();
			Contexto = contexto;
			DtgContratos.ItemsSource = Contexto.Contrato.ToList();
			CbPlan.ItemsSource = Contexto.Plan.ToList();
		}

		private void BtnFiltrar_Click(Object sender, RoutedEventArgs e)
		{
			var filtro = Contexto.Contrato.ToList();

			if(ChkRut.IsChecked.Value)
			{
				if(!Contexto.IsRutValido(TbRut.Text))
				{
					MessageBox.Show("Ingrese un rut válido");
					return;
				}
				filtro = filtro.Where(c => c.RutCliente.Equals(TbRut.Text)).ToList();
			}

			if(ChkNumero.IsChecked.Value)
			{
				var contratos = filtro.Where(c => c.Numero.Equals(TbNumero.Text)).ToList();
				if(contratos.Count == 0)
				{
					MessageBox.Show("Ingrese el número de un contrato válido");
					return;
				}
				filtro = contratos;
			}

			if(ChkRango.IsChecked.Value)
			{
				if (DtpInicioVigencia.DisplayDate > DtpFinVigencia.DisplayDate)
				{
					MessageBox.Show("Ingrese un rango de fechas válido");
					return;
				}
				filtro = filtro.Where(c => 
					(c.FechaInicioVigencia >= DtpInicioVigencia.DisplayDate && c.FechaInicioVigencia <= DtpFinVigencia.DisplayDate)|| 
					(c.FechaFinVigencia >= DtpInicioVigencia.DisplayDate && c.FechaFinVigencia <= DtpFinVigencia.DisplayDate))
					.ToList();
			}

			if(ChkPlan.IsChecked.Value)
			{
				if(CbPlan.SelectedItem as Plan == null)
				{
					MessageBox.Show("Seleccione un plan");
					return;
				}
				filtro = filtro.Where(c => c.Plan == CbPlan.SelectedItem as Plan).ToList();
			}

			if(ChkFiltroVigencia.IsChecked.Value) { filtro = filtro.Where(c => c.Vigente == ChkVigencia.IsChecked.Value).ToList(); }

			DtgContratos.ItemsSource = filtro;
		}

		private void BtnActualizar_Click(Object sender, RoutedEventArgs e)
		{
			if (!(DtgContratos.SelectedItem is Contrato contrato))
			{
				MessageBox.Show("Seleccione un contrato");
				return;
			}
			new ActualizarContrato(Contexto, contrato).ShowDialog();
			BtnFiltrar_Click(this, null);
		}

		private void BtnAnular_Click(Object sender, RoutedEventArgs e)
		{
			if (!(DtgContratos.SelectedItem is Contrato contrato))
			{
				MessageBox.Show("Seleccione un contrato");
				return;
			}
			new AnularContrato(Contexto, contrato);
			BtnFiltrar_Click(this, null);
		}
	}
}
