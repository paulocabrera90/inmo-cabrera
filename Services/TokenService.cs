using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Inmueble_cabrera.Data;

namespace InmobiliariaCA.Services;
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

    public string GenerateJwtToken(string email)
    {
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]  ?? string.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(20),
            claims: claims,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
