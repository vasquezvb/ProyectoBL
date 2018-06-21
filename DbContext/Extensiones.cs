using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BeLifeRe
{
	public static class Extensiones
	{
		public static Boolean IsRutValido(this BeLifeContext self, String rut, Boolean checkExists = true) =>
			rut != null &&
			!rut.Equals(String.Empty) && 
			Regex.IsMatch(rut, "^[0-9]{8}-[1-9kK]$") && 
			(checkExists ? self.Cliente.FirstOrDefault(c => c.RutCliente == rut) != null : true);
	}
}
