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
                Body = MailTemplate.Replace("{TEXT}",mail.Body),
                IsBodyHtml = true,
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
    
    private string MailTemplate = @"
<!DOCTYPE html>
<html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'>
<head>
    <meta charset='utf-8' />
    <meta name='viewport' content='width=device-width' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
    <meta name='x-apple-disable-message-reformatting' />
    <title></title>
    <link href='https://fonts.googleapis.com/css?family=Poppins:200,300,400,500,600,700' rel='stylesheet' />
    <style>
        html, body {
            margin: 0 auto !important;
            padding: 0 !important;
            height: 100% !important;
            width: 100% !important;
            background: #f1f1f1;
        }
        * {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
        }
        div[style*='margin: 16px 0'] {
            margin: 0 !important;
        }
        table, td {
            mso-table-lspace: 0pt !important;
            mso-table-rspace: 0pt !important;
        }
        table {
            border-spacing: 0 !important;
            border-collapse: collapse !important;
            table-layout: fixed !important;
            margin: 0 auto !important;
        }
        img {
            -ms-interpolation-mode: bicubic;
        }
        a {
            text-decoration: none;
        }
        *[x-apple-data-detectors], .unstyle-auto-detected-links *, .aBn {
            border-bottom: 0 !important;
            cursor: default !important;
            color: inherit !important;
            text-decoration: none !important;
            font-size: inherit !important;
            font-family: inherit !important;
            font-weight: inherit !important;
            line-height: inherit !important;
        }
        .a6S {
            display: none !important;
            opacity: 0.01 !important;
        }
        .im {
            color: inherit !important;
        }
        img.g-img + div {
            display: none !important;
        }
        @media only screen and (min-device-width: 320px) and (max-device-width: 374px) {
            u ~ div .email-container {
                min-width: 320px !important;
            }
        }
        @media only screen and (min-device-width: 375px) and (max-device-width: 413px) {
            u ~ div .email-container {
                min-width: 375px !important;
            }
        }
        @media only screen and (min-device-width: 414px) {
            u ~ div .email-container {
                min-width: 414px !important;
            }
        }
        .primary {
            background: #17bebb;
        }
        .bg_white {
            background: #ffffff;
        }
        .bg_light {
            background: #f7fafa;
        }
        .bg_black {
            background: #000000;
        }
        .bg_dark {
            background: rgba(0, 0, 0, 0.8);
        }
        .email-section {
            padding: 2.5em;
        }
        .btn {
            padding: 10px 15px;
            display: inline-block;
        }
        .btn.btn-primary {
            border-radius: 5px;
            background: #17bebb;
            color: #ffffff;
        }
        .btn.btn-white {
            border-radius: 5px;
            background: #ffffff;
            color: #000000;
        }
        .btn.btn-white-outline {
            border-radius: 5px;
            background: transparent;
            border: 1px solid #fff;
            color: #fff;
        }
        .btn.btn-black-outline {
            border-radius: 0px;
            background: transparent;
            border: 2px solid #000;
            color: #000;
            font-weight: 700;
        }
        .btn-custom {
            color: rgba(0, 0, 0, 0.3);
            text-decoration: underline;
        }
        h1, h2, h3, h4, h5, h6 {
            font-family: 'Poppins', sans-serif;
            color: #000000;
            margin-top: 0;
            font-weight: 400;
        }
        body {
            font-family: 'Poppins', sans-serif;
            font-weight: 400;
            font-size: 15px;
            line-height: 1.8;
            color: rgba(0, 0, 0, 0.4);
        }
        a {
            color: #17bebb;
        }
        .hero {
            position: relative;
            z-index: 0;
        }
        .hero .text {
            color: rgba(0, 0, 0, 0.3);
        }
        .hero .text h2 {
            color: #000;
            font-size: 34px;
            margin-bottom: 0;
            font-weight: 200;
            line-height: 1.4;
        }
        .hero .text h3 {
            font-size: 24px;
            font-weight: 300;
        }
        .hero .text h2 span {
            font-weight: 600;
            color: #000;
        }
        .text-author {
            border: 1px solid rgba(0, 0, 0, 0.05);
            max-width: 50%;
            margin: 0 auto;
            padding: 2em;
        }
        .text-author img {
            border-radius: 50%;
            padding-bottom: 20px;
        }
        .text-author h3 {
            margin-bottom: 0;
        }
        ul.social {
            padding: 0;
        }
        ul.social li {
            display: inline-block;
            margin-right: 10px;
        }
        .footer {
            border-top: 1px solid rgba(0, 0, 0, 0.05);
            color: rgba(0, 0, 0, 0.5);
        }
        .footer .heading {
            color: #000;
            font-size: 20px;
        }
        .footer ul {
            margin: 0;
            padding: 0;
        }
        .footer ul li {
            list-style: none;
            margin-bottom: 10px;
        }
        .footer ul li a {
            color: rgba(0, 0, 0, 1);
        }
        @media screen and (max-width: 500px) {}
    </style>
</head>
<body width='100%' style='margin: 0; padding: 0 !important; mso-line-height-rule: exactly; background-color: #f1f1f1;'>
    <center style='width: 100%; background-color: #f1f1f1'>
        <div style='display: none; font-size: 1px; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden; mso-hide: all; font-family: sans-serif;'>
            &zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;
        </div>
        <div style='max-width: 600px; margin: 0 auto' class='email-container'>
            <table align='center' role='presentation' cellspacing='0' cellpadding='0' border='0' width='100%' style='margin: auto'>
                <tr>
                    <td valign='middle' class='hero bg_white' style='padding: 2em 0 4em 0'>
                        <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='100%'>
                            <tr>
                                <td style='padding: 0 2.5em; text-align: center; padding-bottom: 3em'>
                                    <div class='text'>
                                        <h2>{TEXT}</h2>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </center>
</body>
</html>";

}
