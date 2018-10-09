using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace slack_clone_service.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailAddress, string subject, string content);
    }
}