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
        ISMTPWrapper _client;

        public Email(IRepository rep, ILogger log)
        {
            _repository = rep;
            _log = log;
            _client = new SMTPWrapper(_repository, _log);
        }

        // Added for unit tests
        public Email(IRepository rep, ILogger log, ISMTPWrapper client)
        {
            _repository = rep;
            _log = log;
            _client = client;
        }

        public void Send(List<String> contacts, string organisation)
        {
            ValidateEmailAddress(contacts);
            MailMessage email = ComposeEmail(contacts, organisation);
            SmtpClient emailServer = _client.ConfigureSmtpServer();
            emailServer.Send(email);
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

        internal string CreateEmailSubject(string organisation)
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

        internal string CreateEmailBody(string organisation)
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
    }
}
