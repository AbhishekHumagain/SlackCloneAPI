using slack_clone_service.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace slack_clone_service.Service
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public Task SendEmailAsync(string emailAddress, string subject, string content)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_emailConfiguration.SmtpUsername);
                mailMessage.Body = content;
                mailMessage.Subject = subject;

                string[] mulemailIds = emailAddress.Split(',');
                foreach (string item in mulemailIds)
                {
                    mailMessage.To.Add(new MailAddress(item)); //adding multiple TO Email Id
                }
                SmtpClient client = new SmtpClient(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                client.EnableSsl = true;
                client.Send(mailMessage);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}