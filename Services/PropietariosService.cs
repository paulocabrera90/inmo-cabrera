using Inmueble_cabrera.Data;
using Inmueble_cabrera.Models;
using Inmueble_cabrera.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Mysqlx;
using Org.BouncyCastle.Asn1.Cmp;

namespace Inmueble_cabrera.Services;
public class PropietariosService : IPropietariosRepository
{
    private readonly DataContext _context;
    private readonly IEmailSender _emailSender;

    public PropietariosService(DataContext context, IEmailSender emailSender)
    {
        _context = context;
        _emailSender = emailSender;
    }

    public async Task<IEnumerable<Propietario>> GetAllPropietariosAsync()
    {
        return await _context.Propietarios.ToListAsync();
    }

    public async Task<Propietario> GetPropietarioByIdAsync(int id)
    {
        return await _context.Propietarios.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Propietario> CreatePropietarioAsync(Propietario propietario)
    {
        propietario.Password = Commons.CreatePasswordHash(propietario.Password);
        _context.Propietarios.Add(propietario);
        await _context.SaveChangesAsync();
        return propietario;
    }

    public async Task UpdatePropietarioAsync(Propietario propietario)
    {

        {
            _context.Attach(propietario);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeletePropietarioAsync(int id)
    {
        var propietario = await _context.Propietarios.FindAsync(id);
        if (propietario != null)
        {
            _context.Propietarios.Remove(propietario);
            await _context.SaveChangesAsync();
        }
    }

    public bool PropietarioExists(int id)
    {
        return _context.Propietarios.Any(e => e.Id == id);
    }

    public async Task<Propietario> ApplyChanges(Propietario existingPropietario, Propietario propietarioDto)
    {
        // Aplicar solo los campos modificados (si no son null o valores vacíos)
        if (!string.IsNullOrEmpty(propietarioDto.Dni))
            existingPropietario.Dni = propietarioDto.Dni;

        if (!string.IsNullOrEmpty(propietarioDto.Nombre))
            existingPropietario.Nombre = propietarioDto.Nombre;

        if (!string.IsNullOrEmpty(propietarioDto.Apellido))
            existingPropietario.Apellido = propietarioDto.Apellido;

        if (!string.IsNullOrEmpty(propietarioDto.Email))
            existingPropietario.Email = propietarioDto.Email;

        if (!string.IsNullOrEmpty(propietarioDto.Direccion))
            existingPropietario.Direccion = propietarioDto.Direccion;

        if (!string.IsNullOrEmpty(propietarioDto.TelefonoArea))
            existingPropietario.TelefonoArea = propietarioDto.TelefonoArea;

        if (!string.IsNullOrEmpty(propietarioDto.TelefonoNumero))
            existingPropietario.TelefonoNumero = propietarioDto.TelefonoNumero;

        // Actualizar la fecha de modificación
        existingPropietario.FechaActualizacion = DateTime.Now;

        return existingPropietario;
    }

    public async Task<bool> ResetPassword(string email)
    {
        var propietario = await _context.Propietarios.FirstOrDefaultAsync(p => p.Email == email);
        if (propietario == null)
        {
            return false;
        }

        var resetToken = Commons.GenerateVerificationNumber();
        propietario.ResetToken = resetToken;
        propietario.ResetTokenExpires = DateTime.UtcNow.AddHours(1);

        _context.Update(propietario);
        await _context.SaveChangesAsync();

        var mailOptions = new EmailOptions
        {
            From = "no-reply@yourdomain.com",
            To = email,
            Subject = "Recuperación de Contraseña",
            Body = Commons.HtmlBodyEmailRecoveryPassword(resetToken)
        };

        await _emailSender.SendEmailAsync(mailOptions);

        return true;
    }

    public async Task<bool> VerifyNumberStatusAsync(string email, string verificationNumber)
    {
        if (string.IsNullOrWhiteSpace(verificationNumber) || string.IsNullOrWhiteSpace(email))
        {
             return (false);
        }

        var propietario = await _context.Propietarios
                                   .FirstOrDefaultAsync(u => u.ResetToken == verificationNumber && u.Email == email);

        if (propietario == null)
        {
             return (false);
        }

        var status = true;
        var now = DateTime.UtcNow; // Ensure you're using UTC if your server is globally oriented
        if (propietario.ResetTokenExpires.HasValue && propietario.ResetTokenExpires.Value < now)
        {
            status = false;
        }

        return (status);
    }
}


