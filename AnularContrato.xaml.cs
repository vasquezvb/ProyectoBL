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
	/// Lógica de interacción para AnularContrato.xaml
	/// </summary>
	public partial class AnularContrato : MetroWindow
    {
		BeLifeContext Contexto { get; }

		public AnularContrato(BeLifeContext contexto)
		{
			InitializeComponent();
			Contexto = contexto;
		}

		public AnularContrato(BeLifeContext contexto, Contrato contrato) : this(contexto)
		{
			TbNumero.Text = contrato.Numero;
			BtnAnularContrato_Click(this, null);
		}

		private void BtnAnularContrato_Click(Object sender, RoutedEventArgs e)
		{
			var contrato = Contexto.Contrato.FirstOrDefault(c => c.Numero == TbNumero.Text);
			if (TbNumero.Text.Equals(String.Empty) || contrato == null)
			{
				MessageBox.Show("Número de contrato inválido");
				return;
			}

			if(!contrato.Vigente)
			{
				MessageBox.Show("El contrato no se encuentra vigente");
				return;
			}

			var result = MessageBox.Show(
				$"El contrato N°{contrato.Numero} termina el {contrato.FechaFinVigencia} ({(contrato.FechaFinVigencia - DateTime.Now).Days} días)" +
				"\n¿Está seguro que desea anularlo?", "Confirmar anulación de contrato", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if(result == MessageBoxResult.No) { return; }

			contrato.Vigente = false;
			Contexto.SaveChanges();
			MessageBox.Show($"El contrato N°{contrato.Numero} ya no se encuentra vigente", String.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
			Close();
		}
	}
}
