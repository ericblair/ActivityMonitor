using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Configuration;

namespace ActivityMonitor
{
    /// <summary>
    /// Class used to handle contructing and sending of emails
    /// </summary>
    public class Email : ActivityMonitor.IEmail
    {
        IRepository _repository;
        ILogger _log;
        ISMTPWrapper _client;

        #region Constructors

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

        #endregion

        /// <summary>
        /// Send email to contacts passed in via list parameter
        /// Note that this will send to all contacts as 'To' recipients
        /// </summary>
        /// <param name="contacts"></param>
        /// <param name="organisation"></param>
        public void Send(List<String> contacts, string organisation)
        {
            // Construct email
            MailMessage email = ComposeEmail(contacts, organisation);
            //Configure SMTP server
            SmtpClient emailServer = _client.ConfigureSmtpServer();
            // Send email(s)
            emailServer.Send(email);
        }

        /// <summary>
        /// Send email.
        /// This method will send emails to contacts passed in the supplierContacts list as 'To' recipients
        /// and to contacts passed in the healthBoardContacts list as 'CC' recipients
        /// </summary>
        /// <param name="supplierContacts"></param>
        /// <param name="healthBoardContacts"></param>
        /// <param name="organisation"></param>
        public void Send(List<String> supplierContacts, List<String> healthBoardContacts, string organisation)
        {
            // Construct email
            MailMessage email = ComposeEmail(supplierContacts, healthBoardContacts, organisation);
            // configure SMTP server
            SmtpClient emailServer = _client.ConfigureSmtpServer();
            // Send email(s)
            emailServer.Send(email);
        }

        public void SendLog()
        {
            StreamReader _stream = File.OpenText("_log.txt");
            string _text = _stream.ReadToEnd();
            // load contacts
            List<String> _contacts = null;

            _contacts = new List<string>(ConfigurationManager.AppSettings["LogEmailReceipients"].Split(new char[] { ';' }));

            // send
            MailMessage email = new MailMessage();
            MailAddress from = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"]);
            email.From = from;

            MailAddress replyTo = new MailAddress(ConfigurationManager.AppSettings["ReplyToEmailAddress"]);
            email.ReplyTo = replyTo;
            foreach (string address in _contacts)
            {
                MailAddress to = new MailAddress(address);
                email.To.Add(to);
            }
            email.Subject = "Activity Monitor Log File for Date: " + DateTime.Today.ToShortDateString();
            email.Body = _text;

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
            MailAddress from = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"]);
            email.From = from;
            MailAddress replyTo = new MailAddress(ConfigurationManager.AppSettings["ReplyToEmailAddress"]);
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

        private MailMessage ComposeEmail(List<String> supplierRecipients, List<String> healthBoardRecipients, string organisation)
        {
            // Created this method to accomodate sending to 'To' (supplier) and 'CC' (healthBoard) receipients 
            MailMessage email = new MailMessage();
            MailAddress from = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"]);
            email.From = from;
            MailAddress replyTo = new MailAddress(ConfigurationManager.AppSettings["ReplyToEmailAddress"]);
            email.ReplyTo = replyTo;
            foreach (string address in supplierRecipients)
            {
                MailAddress to = new MailAddress(address);
                email.To.Add(to);
            }
            foreach (string address in healthBoardRecipients)
            {
                MailAddress cc = new MailAddress(address);
                email.CC.Add(cc);
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
                string _supplierRef = _repository.GetOrganisationSupplierReference(organisation);

                if (_supplierRef == null)
                    throw new Exception("No Supplier Reference could be found to create subject for EMIS inactive report email.");

                _subject = "Transmission Fault in ePharmacy CDB" + _supplierRef + " (OrgID " + organisation.Trim() + ")";
            }
            else if (_supplier == "INPS")
            {
                string _supplierRef = _repository.GetOrganisationSupplierReference(organisation);

                if (_supplierRef == null)
                    throw new Exception("No Supplier Reference could be found to create subject for INPS inactive report email.");

                _subject = "eAMS Offline - INPS Reference " + _supplierRef + " (OrgID " + organisation.Trim() + ")";
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
                        + "This site ("
                        + _repository.GetOrganisationName(organisation) + ", CDB"
                        + _repository.GetOrganisationSupplierReference(organisation) + ", OrgID "
                        +  organisation.Trim()
                        + ") is reported as being offline. The last AMS message was received on "
                        + _repository.GetDateTimeOfLatestAMSMessage(organisation)
                        + "\n Please arrange for this to be investigated and brought back online as soon as possible.";
            }
            else if (_supplier == "INPS")
            {
                _body = "eAMS Offline\n\n"
                        + "This site ("
                        + _repository.GetOrganisationName(organisation) + ", INPS Reference "
                        + _repository.GetOrganisationSupplierReference(organisation) + ", OrgID "
                        + organisation.Trim()
                        + ") is reported as being offline. The last AMS message was received on "
                        + _repository.GetDateTimeOfLatestAMSMessage(organisation)
                        + "\n Please arrange for this to be investigated and brought back online as soon as possible.";
            }
            else
            {
                _body = "Site: " + organisation + "is currently inactive";
            }

            return _body;
        }
    }
}
