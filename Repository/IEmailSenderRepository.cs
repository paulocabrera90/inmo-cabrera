
namespace Inmueble_cabrera.Repository;
public interface IEmailSenderRepository
{
    Task SendEmailAsync(EmailOptions mailOptions);
}