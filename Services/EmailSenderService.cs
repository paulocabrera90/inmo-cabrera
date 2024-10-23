using MailKit.Net.Smtp;
using MimeKit;

public class EmailSenderService : IEmailSender
{
    public async Task SendEmailAsync(EmailOptions mailOptions)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Nombre del Remitente", mailOptions.From)); // Cambiado aquí
        emailMessage.To.Add(new MailboxAddress("Nombre del Destinatario", mailOptions.To)); // Cambiado aquí
        emailMessage.Subject = mailOptions.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mailOptions.Body };

        using (var client = new SmtpClient())
        {
            // Aquí debes colocar tus configuraciones SMTP
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("nicolas.cabrera@redb.ee", "hsbzwqsiuxpwrolb");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}