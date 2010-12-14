using System;
namespace ActivityMonitor
{
    public interface IEmail
    {
        // System.Net.Mail.MailMessage ComposeEmail(System.Collections.Generic.List<string> recipients, string organisation);
        System.Net.Mail.SmtpClient ConfigureSmtpServer();
        bool ValidateEmailAddress(System.Collections.Generic.List<string> contacts);
        void Send(System.Collections.Generic.List<String> contacts, string organisation);
    }
}
