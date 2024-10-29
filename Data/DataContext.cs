namespace Inmueble_cabrera.Data;

using Inmueble_cabrera.Models;
using Inmueble_cabrera.Models.ContratoModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class DataContext : DbContext
{

	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{ }

	public DbSet<Propietario> Propietarios { get; set; }
	public DbSet<TipoInmueble> Tipos_Inmueble { get; set; }
	public DbSet<Inmueble> Inmuebles { get; set; }
	public DbSet<TipoInmuebleUso> Tipos_Inmueble_Uso { get; set; }
	public DbSet<Contrato> Contratos { get; set; }

	public DbSet<Inquilino> Inquilinos { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Configuración específica para la conversión de EstadoContrato a string
		modelBuilder.Entity<Contrato>()
		.Property(p => p.Estado)
		.HasConversion(new ValueConverter<EstadoContrato, string>(
			v => v.ToString(),
			v => (EstadoContrato)Enum.Parse(typeof(EstadoContrato), v)
		));
	}

}