using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Mojo.Application.Model;
using Mojo.Application.Persistance.Emails;

namespace Mojo.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        public EmailSettings _emailSettings { get; }

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendEmail(EmailMessage email)
        {
            Console.WriteLine("=== DÉBUT ENVOI EMAIL ===");
            Console.WriteLine($"To: {email.To}");
            Console.WriteLine($"Subject: {email.Subject}");
            Console.WriteLine($"SmtpServer: {_emailSettings.SmtpServer}");
            Console.WriteLine($"SmtpPort: {_emailSettings.SmtpPort}");
            Console.WriteLine($"SmtpUsername: {_emailSettings.SmtpUsername}");
            Console.WriteLine($"FromAddress: {_emailSettings.FromAddress}");

            try
            {
                using (var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(
                        _emailSettings.SmtpUsername,
                        _emailSettings.SmtpPassword
                    );

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_emailSettings.FromAddress, _emailSettings.FromName),
                        Subject = email.Subject,
                        Body = email.Body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(email.To);

                    Console.WriteLine("Envoi de l'email en cours...");
                    await smtpClient.SendMailAsync(mailMessage);
                    Console.WriteLine("✅ EMAIL ENVOYÉ AVEC SUCCÈS !");

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("=== ❌ ERREUR ENVOI EMAIL ===");
                Console.WriteLine($"Type: {ex.GetType().Name}");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"InnerException: {ex.InnerException?.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                Console.WriteLine("============================");
                return false;
            }
        }
    }
}