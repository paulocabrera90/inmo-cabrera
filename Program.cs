using System.Text;
using Inmueble_cabrera.Models;
using Inmueble_cabrera.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:5000");
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
    //c.EnableAnnotations();
});

builder.Services.AddDbContext<Inmueble_cabrera.Data.DataContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 21))));

 // Asegúrate de registrar tu servicio aquí
builder.Services.AddScoped<IPropietariosRepository, PropietariosService>();
builder.Services.AddScoped<IInmueblesRepository, InmueblesService>();
builder.Services.AddScoped<IEmailSender, EmailSenderService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

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

app.UseCors(x => x
	.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader());
    
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
