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
        SmtpClient _client;

        public Email(IRepository rep, ILogger log)
        {
            _repository = rep;
            _log = log;
            _client = new SmtpClient();
        }

        // Added for unit tests
        public Email(IRepository rep, ILogger log, SmtpClient client)
        {
            _repository = rep;
            _log = log;
            _client = client;
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

        private MailMessage ComposeEmail(List<String> recipients, string organisation)
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
            email.Subject = CreateEmailSubject(organisation);
            email.Body = CreateEmailBody(organisation);

            return email;
        }

        private string CreateEmailSubject(string organisation)
        {
            string _supplier = _repository.GetOrganisationSupplier(organisation);
            string _subject = null;

            if (_supplier == "EMIS")
            {
                _subject = _repository.GetOrganisationSupplierReference(organisation);

                if (_subject == null) 
                    throw new Exception("No Supplier Reference could be found to create subject for EMIS inactive report email.");
            }
            else
            {
                _subject = "Inactive Organisation:" + organisation;
            }

            return _subject;
        }

        private string CreateEmailBody(string organisation)
        {
            string _supplier = _repository.GetOrganisationSupplier(organisation);
            string _body = null;

            if (_supplier == "EMIS")
            {
                _body = "Transmission Fault in ePharmacy\n\n"
                        + "Site ("
                        + _repository.GetOrganisationName(organisation) + ", "
                        + _repository.GetOrganisationSupplierReference(organisation) + ", "
                        +  organisation
                        + ") is reported as being offline. The last AMS [or CMS] message was received on DD/MM/YYYY. Please arrange"
                        + "for this to be investigated and brought back online as soon as possible.";
            }
            else
            {
                _body = "Site: " + organisation + "is currently inactive";
            }

            return _body;
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
