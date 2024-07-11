namespace SwordTech.Melodart.Helper.Mail;

public interface IMailService
{
    Task SendMail(Mail mail);
}


public class Mail
{
    public string Title { get; set; }
    public string Subject { get; set; }
    public string Email { get; set; }
    public string Body { get; set; }
}