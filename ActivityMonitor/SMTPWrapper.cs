using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace ActivityMonitor
{
    public class SMTPWrapper : ActivityMonitor.ISMTPWrapper
    {
        private SmtpClient _client;
        IRepository _repository;
        ILogger _log;

        public SMTPWrapper(IRepository rep, ILogger log)
        {
            _repository = rep;
            _log = log;
        }

        // Unit testing
        public SMTPWrapper() { }

        // Unit testing
        public SMTPWrapper(IRepository rep, ILogger log, SmtpClient client)
        {
            _repository = rep;
            _log = log;
            _client = client;
        }

        public SmtpClient ConfigureSmtpServer()
        {
            // string _smtpIP = _repository.GetSMTPIPAddress();
            // int _smtpPort = _repository.GetSMTPPortNumber();
            // Create SMTP client at mail server location
            //_client = new SmtpClient(_smtpIP, _smtpPort);
            // Add credentials
            //_client.UseDefaultCredentials = true;

            // The block commented-out above works (hence it not being deleted)
            // Moved smtp settings to config file, these are called by using the default constructor for smtpclient
            // The reason for doing this was to try and get papercut running
            _client = new SmtpClient();

            return _client;
        }

        public void Send(MailMessage msg)
        {
            _client.Send(msg);
        }
    }
}
