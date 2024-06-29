using Me.Cv.Domain.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Me.Cv.Infrastructure.Emails;

public class EmailService(
    EmailSettings emailSettings) : IEmailService
{
    public async Task Send(Domain.Entities.Email email)
    {
        using (var mailMessage = new MailMessage(emailSettings.UserName, email.To))
        {
            var from = new MailAddress(emailSettings.UserName, "Mi CV - Contacto");
            mailMessage.From = from;
            mailMessage.Subject = email.Subject;
            mailMessage.Body = email.Body;
            mailMessage.IsBodyHtml = true;
            using (var smtpClient = new SmtpClient(emailSettings.Smpt, emailSettings.Port))
            {
                smtpClient.EnableSsl = true;
                var networkCredential = new NetworkCredential(emailSettings.UserName, emailSettings.Password);
                smtpClient.Credentials = networkCredential;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
            }
        }
    }
}
