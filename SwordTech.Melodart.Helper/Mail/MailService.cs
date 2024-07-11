using System.Net;
using System.Net.Mail;

namespace SwordTech.Melodart.Helper.Mail;

public class MailService : IMailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;

    public MailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
    {
        _smtpServer = smtpServer ?? throw new ArgumentNullException(nameof(smtpServer));
        _smtpPort = smtpPort;
        _smtpUser = smtpUser ?? throw new ArgumentNullException(nameof(smtpUser));
        _smtpPass = smtpPass ?? throw new ArgumentNullException(nameof(smtpPass));
    }

    public async Task SendMail(Mail mail)
    {
        try
        {
            var smtpClient = new SmtpClient(_smtpServer)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true,
                Host = _smtpServer
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUser),
                Subject = mail.Subject,
                Body = mail.Body,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(mail.Email);

            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            // Log the exception (if a logging framework is in place)
            Console.WriteLine($"Exception caught in SendMail: {ex}");
        }
    }
}
