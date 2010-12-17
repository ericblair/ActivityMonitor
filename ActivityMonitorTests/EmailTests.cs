using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using ActivityMonitorTests.TestHelpers;

namespace ActivityMonitorTests
{
    [TestClass]
    public class EmailTests
    {
        private IEPMS_StatisticsEntities _mockContext;
        private Mock<ILogger> _log;
        private ActivityMonitor.Repository.Repository _repository;
        private Mock<ISMTPWrapper> _smtpClient;
        private Email _email;

        private List<String> _contacts;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new EPMS_StatisticsEntitiesMock();
            _log = new Mock<ILogger>();
            _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);
            _smtpClient = new Mock<ISMTPWrapper>();
            _email = new Email(_repository, _log.Object, _smtpClient.Object);

            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "INPS"));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("2345", "EMIS"));

            _mockContext.tbOrganisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1234", "Highland Health Board", "Non-EMIS Site"));
            _mockContext.tbOrganisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("2345", "Highland Health Board", "EMIS Site"));

            _contacts = new List<string>();
            _contacts.Add("test1@contacts.com");
            _contacts.Add("test2@contacts.com");
        }

        [TestMethod]
        public void Email_CreateEmailSubject_OrganisationSupplierNotEMIS()
        {
            string _organisation = "1234";
            string _subject = _email.CreateEmailSubject(_organisation);

            Assert.AreEqual(_subject, "Inactive Organisation:" + _organisation);
        }

        [TestMethod]
        public void Email_CreateEmailSubject_OrganisationSupplierIsEMIS()
        {
            string _organisation = "2345";

            string _subject = _email.CreateEmailSubject(_organisation);

            //Assert.AreEqual(_subject, "EMIS Site");
            Assert.AreEqual(_subject, "Transmission Fault in ePharmacy CDB" + "EMIS Site" + " (OrgID " + _organisation + ")");
        }

        [TestMethod]
        public void Email_CreateEmailBody_OrganisationSupplierNotEMIS()
        {
            string _organisation = "1234";

            string _body = _email.CreateEmailBody(_organisation);

            Assert.AreEqual(_body, "Site: " + _organisation + "is currently inactive");
        }

        [TestMethod]
        public void Email_CreateEmailBody_OrganisationSupplierIsEMIS()
        {
            string _organisation = "2345";

            string _body = _email.CreateEmailBody(_organisation);

            string _expectedMessage = "Transmission Fault in ePharmacy\n\n"
                        + "This site ("
                        + _repository.GetOrganisationName(_organisation) + ", CDB"
                        + _repository.GetOrganisationSupplierReference(_organisation) + ", OrgID "
                        + _organisation
                        + ") is reported as being offline. The last AMS message was received on "
                        + DateTime.Today.AddDays(-3).ToShortDateString()
                        + "\n Please arrange for this to be investigated and brought back online as soon as possible.";

            Assert.AreEqual(_body, _expectedMessage);
        }

        [TestMethod]
        public void Email_Send_EmailIsSentToContacts()
        {
            //string _organisation = "1234";

            //string _ip = "127.0.0.1";
            //int _port = 0;
            //SmtpClient _testSmtpClient  = new SmtpClient(_ip, _port);

            //MailMessage _emailMessage = new MailMessage();
            //_emailMessage.From = new MailAddress("ePharmacyReports@eps.nds.scot.nhs.uk");
            //_emailMessage.ReplyTo = new MailAddress("NSS.PSDHelp@nhs.net");
            //_emailMessage.To.Add("test1@contacts.com");
            //_emailMessage.To.Add("test2@contacts.com");
            //_emailMessage.Subject = _email.CreateEmailSubject(_organisation);
            //_emailMessage.Body = _email.CreateEmailBody(_organisation);

            //_smtpClient.Setup(client => client.ConfigureSmtpServer()).Returns(_testSmtpClient);
            ////_smtpClient.Setup(client => client.ConfigureSmtpServer()).Returns(new SmtpClient());
            //_smtpClient.Setup(client => client.Send(It.IsAny<MailMessage>())).Verifiable();

            //_email.Send(_contacts, _organisation);

            //_smtpClient.Verify();
        }
    }
}
