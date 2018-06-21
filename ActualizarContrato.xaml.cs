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

namespace BeLifeRe
{
	/// <summary>
	/// Lógica de interacción para ActualizarContrato.xaml
	/// </summary>
	public partial class ActualizarContrato : Window
	{
		BeLifeContext Contexto { get; }

		Contrato CurrentContrato { get; set; }

		public ActualizarContrato(BeLifeContext contexto)
		{
			InitializeComponent();
			Contexto = contexto;
			CbPlan.ItemsSource = Contexto.Plan.ToList();
		}

		public ActualizarContrato(BeLifeContext contexto, Contrato contrato) : this(contexto)
		{
			TbNumero.IsEnabled = false;
			TbNumero.Text = contrato.Numero;
			IsVisibleChanged += ActualizarContrato_IsVisibleChanged;
		}

		private void ActualizarContrato_IsVisibleChanged(Object sender, DependencyPropertyChangedEventArgs e) => BtnNumero_Click(this, null);

		private void BtnNumero_Click(Object sender, RoutedEventArgs e)
		{
			CurrentContrato = Contexto.Contrato.FirstOrDefault(c => c.Numero.Equals(TbNumero.Text));
			if(CurrentContrato == null)
			{
				MessageBox.Show("Ingrese el código de un contrato válido");
				return;
			}

			CbPlan.SelectedItem = CurrentContrato.Plan;

			var dt = DateTime.Now;
			DtpFinVigencia.DisplayDateStart = CurrentContrato.FechaFinVigencia > dt ? CurrentContrato.FechaFinVigencia : dt;
			DtpFinVigencia.DisplayDateEnd = DtpFinVigencia.DisplayDateStart + TimeSpan.FromDays(366);
		}

		private void BtnContratoActualizar_Click(Object sender, RoutedEventArgs e)
		{
			var fechaFin = DtpFinVigencia.DisplayDate;
			if (!(CbPlan.SelectedItem is Plan plan))
			{
				MessageBox.Show("Seleccione un plan");
				return;
			}
			if (plan.Equals(CurrentContrato.Plan) && fechaFin.Equals(CurrentContrato.FechaFinVigencia))
			{
				MessageBox.Show("No hay datos para modificar");
				return;
			}

			var result = MessageBox.Show(
				$"Se actualizarán los siguientes datos del contrato N°{CurrentContrato.Numero}:" +
				(plan.Equals(CurrentContrato.Plan) ? "" : $"\n  Plan asociado : {CurrentContrato.Plan} -> {plan}") +
				(fechaFin.Equals(CurrentContrato.FechaFinVigencia) ? "" : $"\n  Fin vigencia : {CurrentContrato.FechaFinVigencia} -> {fechaFin}")
				, "Confirmar actualización de datos", MessageBoxButton.OKCancel);

			if (result == MessageBoxResult.OK)
			{ 
				if (!fechaFin.Equals(CurrentContrato.FechaFinVigencia) && !CurrentContrato.Vigente)
				{
					CurrentContrato.Vigente = true;
				}
				CurrentContrato.Plan = plan;
				CurrentContrato.FechaFinVigencia = fechaFin;
				Contexto.SaveChanges();
			}
			Close();
		}
	}
}
