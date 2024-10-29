using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Inmueble_cabrera.Data;
using Inmueble_cabrera.Models;

namespace Inmueble_cabrera.Services;
public class TokenService
{
    private DataContext context;
    private IConfiguration config;
    private IWebHostEnvironment environment;

    public TokenService(DataContext context, IConfiguration config, IWebHostEnvironment environment)
    {
        this.context = context;
        this.config = config;
        this.environment = environment;
    }

    public string GenerateJwtToken(Propietario user)
    {
        var claims = new List<Claim>
        {
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Email, user.Email),
		new Claim("FullName", user.NombreCompleto()),
		new Claim("Id_propietario", user.Id.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]  ?? string.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(30),
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
