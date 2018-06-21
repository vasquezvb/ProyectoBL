namespace BeLifeRe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contrato")]
    public partial class Contrato
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(14)]
        public string Numero { get; set; }

        public DateTime FechaCreacion { get; set; }
		
        [Column(Order = 1)]
        [StringLength(10)]
        public string RutCliente { get; set; }
		
        [Column(Order = 2)]
        [StringLength(5)]
        public string CodigoPlan { get; set; }

        public DateTime FechaInicioVigencia { get; set; }

        public DateTime FechaFinVigencia { get; set; }

        public bool Vigente { get; set; }

        public bool DeclaracionSalud { get; set; }

        public double PrimaAnual { get; set; }

        public double PrimaMensual { get; set; }

        [Required]
        public string Observaciones { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Plan Plan { get; set; }
    }
}
