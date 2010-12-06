using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace ActivityMonitor
{
    class ReportInactiveSites
    {
        IRepository _repository;
        ILogger _log;

        public ReportInactiveSites(IRepository rep, ILogger log)
        {
            _repository = rep;
            _log = log;
        }

        public void SendInactiveReports()
        {
            List<String> _organisations = GetAllInactiveSites();
            foreach (string _organisation in _organisations)
            {
                List<String> _contacts = GetContactsForOrganisation(_organisation);
                ValidateEmailAddress(_contacts);    // This may not be the best place to do this as the same addresses will be constantly re-evaluated

                MailMessage email = ComposeEmail(_contacts, _organisation);
                SmtpClient client = ConfigureSmtpServer();
                client.Send(email);

                RecordOrganisationInactiveReportHasBeenSent(_organisation);
                _log.Add("Inactive email report was sent for site: " + _organisation);
            }
        }

        private List<String> GetAllInactiveSites()
        {
            return _repository.GetNewlyInactiveSites();
        }

        private List<String> GetContactsForOrganisation(string organisation)
        {
            List<String> _contacts = new List<string>();

            string _supplier = _repository.GetOrganisationSupplier(organisation);
            string _healthBoard = _repository.GetOrganisationHealthBoard(organisation);

            _contacts.AddRange(_repository.GetSupplierContactsEmailAddresses(_supplier));
            _contacts.AddRange(_repository.GetHealthBoardContactsEmailAddresses(_healthBoard));

            return _contacts;
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

        private bool ValidateEmailAddress(List<String> contacts)
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
            email.Subject = "Inactive Organisation:" + organisation;
            email.Body = "Site: " + organisation + "is currently inactive";

            return email;
        }

        private SmtpClient ConfigureSmtpServer()
        {
            // Create SMTP client at mail server location
            SmtpClient _client = new SmtpClient("192.168.4.190", 25);
            // Add credentials
            _client.UseDefaultCredentials = true;

            return _client;
        }

        private void RecordOrganisationInactiveReportHasBeenSent(string organisation)
        {
            _repository.RecordDateInactiveWarningEmailWasSent(organisation);
        }
    }
}
