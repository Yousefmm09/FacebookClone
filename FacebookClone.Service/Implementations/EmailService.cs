using Azure.Messaging;
using FacebookClone.Service.Abstract;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore.Storage.Json;
using  MimeKit;
using MimeKit;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Text;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace FacebookClone.Service.Implementations
{
    public class EmailService:IEmailService
    {
        
        public async Task<string> SendEmail(string email, string message)
        {
            using( var send= new SmtpClient ())
            {
                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync("yousefmohsen232@gmail.com", "qgpydigixhizwkey");

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = message,
                    TextBody = "FaceBookClone"
                };

                var mail = new MimeMessage()
                {
                    Body = bodyBuilder.ToMessageBody(),
                    Subject = "Facebook Clone"
                };

                mail.From.Add(new MailboxAddress("Facebook Clone", "yousefmohsen232@gmail.com"));
                mail.To.Add(new MailboxAddress("", email));
                mail.Subject = message;
                mail.Body = bodyBuilder.ToMessageBody();

                await client.SendAsync(mail);
                await client.DisconnectAsync(true);
            }
            return "Email Sent Successfully";
        }
    }
}
