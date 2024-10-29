namespace Inmueble_cabrera.Data;

using Inmueble_cabrera.Models;
using Inmueble_cabrera.Models.ContratoModels;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext  
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
	{}

    public DbSet<Propietario> Propietarios { get; set; }
	public DbSet<TipoInmueble> Tipos_Inmueble { get; set; }
	public DbSet<Inmueble> Inmuebles { get; set; }
	public DbSet<TipoInmuebleUso> Tipos_Inmueble_Uso { get; set; }
	public DbSet<Contrato> Contratos { get; set; }

	public DbSet<Inquilino> Inquilinos { get; set; }



}