namespace BeLifeRe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cliente")]
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            Contrato = new HashSet<Contrato>();
        }

        [Key]
        [StringLength(10)]
        public string RutCliente { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(20)]
        public string Apellidos { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public int IdSexo { get; set; }

        public int IdEstadoCivil { get; set; }

        public virtual EstadoCivil EstadoCivil { get; set; }

        public virtual Sexo Sexo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contrato> Contrato { get; set; }

		public override String ToString() => $"{Apellidos}, {Nombres}";
	}
}
