public interface IEmailSender
{
    Task SendEmailAsync(EmailOptions mailOptions);
}