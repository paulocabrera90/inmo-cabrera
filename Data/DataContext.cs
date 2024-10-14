namespace Inmueble_cabrera.Data;

using InmobiliariaCA.Models;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext  
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
	{}

    public DbSet<Propietario> Propietarios { get; set; }
	public DbSet<TipoInmueble> TipoInmueble { get; set; }
	public DbSet<Inmueble> Inmuebles { get; set; }
	public DbSet<TipoInmuebleUso> TipoInmuebleUso { get; set; }


}