//using MailKit.Net.Smtp;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using MimeKit;
//using SendGrid;
//using SendGrid.Helpers.Mail;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ABBY.UTILITY
{
    //public class EmailSender : IEmailSender
    //{
    //    public string SendGridSecret { get; set; }
    //    public EmailSender(IConfiguration _config)
    //    {
    //        SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
    //    }
    //    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    //    {

    //        //var emailToSend = new MimeMessage();
    //        //emailToSend.From.Add(MailboxAddress.Parse("shank271424@gmail.com"));
    //        //emailToSend.To.Add(MailboxAddress.Parse(email));
    //        //emailToSend.Subject = subject;
    //        //emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };


    //        ////send email

    //        //using(var emailClient = new SmtpClient())
    //        //{
    //        //    emailClient.Connect("smtp@gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
    //        //    emailClient.Authenticate("shank271424@gmail.com", "27121998Sh@27");
    //        //    emailClient.Send(emailToSend);
    //        //    emailClient.Disconnect(true);
    //        //}
    //        var client = new SendGridClient(SendGridSecret);
    //        var from = new EmailAddress("Shashank.Dhakate@dentsu.com", "Abby");
    //        var to = new EmailAddress(htmlMessage);
    //        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

    //        return client.SendEmailAsync(msg);
    //        return Task.CompletedTask;
    //    }
    //}

    public class EmailSender : IEmailSender
    {
        public string SendGridSecret { get; set; }
        public EmailSender(IConfiguration _config)
        {
            SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
        }



        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {

            await Execute(SendGridSecret, subject, message, toEmail);

        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("Shashank.Dhakate@gmail.com", "Password Recovery"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));


            msg.SetClickTracking(false, false);
            await client.SendEmailAsync(msg);

        }
    }
}
