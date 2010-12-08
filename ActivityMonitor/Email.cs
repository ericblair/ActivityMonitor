using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace ActivityMonitor
{
    public class Email : ActivityMonitor.IEmail
    {
        IRepository _repository;
        ILogger _log;

        public Email(IRepository rep, ILogger log)
        {
            _repository = rep;
            _log = log;
        }

        public void Send(List<String> contacts, string organisation)
        {
            ValidateEmailAddress(contacts);
            MailMessage email = ComposeEmail(contacts, organisation);
            SmtpClient client = ConfigureSmtpServer();
            client.Send(email);
        }

        private bool ValidateEmailAddress(string emailAddress)
        {
            string textToValidate = emailAddress;
            Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

            // Test email address with expression
            if (expression.IsMatch(textToValidate))
            {
                // Is valid address
                return true;
            }
            else
            {
                // Log and throw error 
                _log.Add("ERROR: Invalid email address detected. value = " + textToValidate);
                return false;
            }
        }

        public bool ValidateEmailAddress(List<String> contacts)
        {
            // bool allContactsAreValid = false;

            foreach (string emailAddress in contacts)
            {
                if (!ValidateEmailAddress(emailAddress))
                    return false;
            }
            return true;
        }

        public MailMessage ComposeEmail(List<String> recipients, string organisation)
        {
            // Create the email message
            MailMessage email = new MailMessage();
            MailAddress from = new MailAddress("ePharmacyReports@eps.nds.scot.nhs.uk");
            email.From = from;
            MailAddress replyTo = new MailAddress("NSS.PSDHelp@nhs.net");
            email.ReplyTo = replyTo;
            foreach (string address in recipients)
            {
                MailAddress to = new MailAddress(address);
                email.To.Add(to);
            }
            email.Subject = "Inactive Organisation:" + organisation;
            email.Body = "Site: " + organisation + "is currently inactive";

            return email;
        }

        public SmtpClient ConfigureSmtpServer()
        {
            // Create SMTP client at mail server location
            SmtpClient _client = new SmtpClient("192.168.4.190", 25);
            // Add credentials
            _client.UseDefaultCredentials = true;

            return _client;
        }
    }
}
