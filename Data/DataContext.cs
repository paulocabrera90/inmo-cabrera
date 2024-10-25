namespace Inmueble_cabrera.Data;

using Inmueble_cabrera.Models;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext  
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
	{}

    public DbSet<Propietario> Propietarios { get; set; }
	public DbSet<TipoInmueble> Tipos_Inmueble { get; set; }
	public DbSet<Inmueble> Inmuebles { get; set; }
	public DbSet<TipoInmuebleUso> Tipos_Inmueble_Uso { get; set; }


}