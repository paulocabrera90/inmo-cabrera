using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inmobiliaria Cabrera", Version = "v1" });
    c.SwaggerDoc("v1", new OpenApiInfo 
    {
        Version = "v1",
        Title = "Inmobiliaria Cabrera",
        Description = "An ASP.NET Core Web API for managing Inmobiliaria items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Laboratorio III",
            Url = new Uri("https://www.campusvirtual.ulp.edu.ar/programas.cgi?id_curso=1561")
        },
        License = new OpenApiLicense
        {
            Name = "Linkedin",
            Url = new Uri("https://www.linkedin.com/in/pncabrera/")
        }
    });
});

builder.Services.AddDbContext<Inmueble_cabrera.Data.DataContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 21))));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inmobiliaria Cabrera V1"));
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
