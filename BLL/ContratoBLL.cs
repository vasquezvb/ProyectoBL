using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ContratoBLL
    {
        public string Numero  { get; set; }
        public DateTime FechaCreacion  { get; set; }
        public DateTime FechaTermino  { get; set; }
        public string RutCliente  { get; set; }
        public string  CodigoPlan   { get; set; }
        public int  IdTipoContrato{ get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }
        public bool Vigente { get; set; }
        public bool DeclaracionSalud { get; set; }
        public float PrimaAnual { get; set; }
        public float PrimaMensual { get; set; }
        public string Observaciones { get; set; }

    }
}
