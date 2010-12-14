using System;
namespace ActivityMonitor
{
    public interface ISMTPWrapper
    {
        System.Net.Mail.SmtpClient ConfigureSmtpServer();
        void Send(System.Net.Mail.MailMessage msg);
    }
}
