namespace BeLifeRe
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class BeLifeContext : DbContext
	{
		public BeLifeContext()
			: base("name=BeLifeContext")
		{
		}

		public virtual DbSet<Cliente> Cliente { get; set; }
		public virtual DbSet<Contrato> Contrato { get; set; }
		public virtual DbSet<EstadoCivil> EstadoCivil { get; set; }
		public virtual DbSet<Plan> Plan { get; set; }
		public virtual DbSet<Sexo> Sexo { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Cliente>()
				.HasMany(e => e.Contrato)
				.WithRequired(e => e.Cliente)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<EstadoCivil>()
				.HasMany(e => e.Cliente)
				.WithRequired(e => e.EstadoCivil)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Plan>()
				.HasMany(e => e.Contrato)
				.WithRequired(e => e.Plan)
				.HasForeignKey(e => e.CodigoPlan)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Sexo>()
				.HasMany(e => e.Cliente)
				.WithRequired(e => e.Sexo)
				.WillCascadeOnDelete(false);
		}
	}
}
