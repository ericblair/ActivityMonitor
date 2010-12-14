using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using Moq;
using ActivityMonitorTests.TestHelpers;

namespace ActivityMonitorTests
{
    [TestClass()]
    public class ReportInactiveSitesTest
    {
        private IEPMS_StatisticsEntities _mockContext;
        private Mock<ILogger> _log;
        // private Mock<IRepository> _repository;

        private ReportInactiveSites _reportInactiveSites;
        private Mock<IEmail> _email;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new EPMS_StatisticsEntitiesMock();
            _log = new Mock<ILogger>();
            // _repository = new Mock<IRepository>(_log.Object, _mockContext);
            // _repository = new Mock<IRepository>();
            // _reportInactiveSites = new ReportInactiveSites(_repository.Object, _log.Object);
            _email = new Mock<IEmail>();

            // _mockContext.tbInactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow())
        }

        [TestMethod()]
        public void SendInactiveReports_NoInactiveOrganisations_NoReportsSent()
        {
            Mock<IRepository> _repository = new Mock<IRepository>();
            _reportInactiveSites = new ReportInactiveSites(_repository.Object, _log.Object, _email.Object);

            List<String> _emptyList = new List<string>();   
            _repository.Setup(rep => rep.GetNewlyInactiveSites()).Returns(_emptyList);

            _reportInactiveSites.SendInactiveReports();
            _email.Verify(email => email.Send(_emptyList, It.IsAny<string>()), Times.Never());
            //_email.Verify(email => email.Send(It.IsAny<List<string>>, It.IsAny<string>()), Times.Never());
            _log.Verify(log => log.Add("INFO: There are no newly inactive sites to send reports to."));
        }

        [TestMethod]
        public void SendInactiveReports_InactiveOrgHasNoContacts_NoReportsSent()
        {
            // Going to need a proper repository for this test
            ActivityMonitor.Repository.Repository _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);
            _reportInactiveSites = new ReportInactiveSites(_repository, _log.Object, _email.Object);

            _mockContext.tbInactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("1234", null, DateTime.Today, null));
            List<String> _emptyList = new List<string>();  

            _reportInactiveSites.SendInactiveReports();
            _email.Verify(email => email.Send(_emptyList, It.IsAny<string>()), Times.Never());
            _log.Verify(log => log.Add("WARNING: No contacts for organisation: " + "1234" + " could be found."));
        }

        [TestMethod]
        public void SendInactiveReports_MultipleInactiveOrgs_OneOrgHasNoContacts_EmailSentToOrgWithContacts()
        {
            ActivityMonitor.Repository.Repository _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);
            _reportInactiveSites = new ReportInactiveSites(_repository, _log.Object, _email.Object);

            _mockContext.tbInactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("1234", null, DateTime.Today, null));
            _mockContext.tbInactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("2345", null, DateTime.Today, null));

            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("2345", "INPS"));
            _mockContext.tbOrganisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("2345", "Highland Health Board"));

            _mockContext.tbHealthBoardContacts.AddObject(TestHelpers.PopulateTable.AddHealthBoardContactsDataRow(1, "Highland Health Board", "high@land.com"));
            _mockContext.tbSupplierContacts.AddObject(TestHelpers.PopulateTable.AddSupplierContactsDataRow(1, "INPS", "test@INPS.com"));

            List<String> _contacts = new List<string>();
            _contacts.Add("test@INPS.com");
            _contacts.Add("high@land.com");

            _reportInactiveSites.SendInactiveReports();
            _log.Verify(log => log.Add("WARNING: No contacts for organisation: " + "1234" + " could be found."));
            _email.Verify(email => email.Send(_contacts, "2345"), Times.Exactly(1));
            _log.Verify(log => log.Add("Inactive email report was sent for site: " + "2345"));
            Assert.AreEqual(_mockContext.tbInactiveSites.ElementAt(1).DateEmailSent, DateTime.Today);
        }

        [TestMethod]
        public void SendInactiveReports_ErrorRaisedWhenSendingEmail_ErrorCaughtAndLogged()
        {
            ActivityMonitor.Repository.Repository _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);
            _reportInactiveSites = new ReportInactiveSites(_repository, _log.Object, _email.Object);

            _mockContext.tbInactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("2345", null, DateTime.Today, null));

            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("2345", "INPS"));
            _mockContext.tbOrganisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("2345", "Highland Health Board"));

            _mockContext.tbHealthBoardContacts.AddObject(TestHelpers.PopulateTable.AddHealthBoardContactsDataRow(1, "Highland Health Board", "high@land.com"));
            _mockContext.tbSupplierContacts.AddObject(TestHelpers.PopulateTable.AddSupplierContactsDataRow(1, "INPS", "test@INPS.com"));

            List<String> _contacts = new List<string>();
            _contacts.Add("test@INPS.com");
            _contacts.Add("high@land.com");

            _email.Setup(email => email.Send(_contacts, "2345")).Throws(new Exception("Error sending email"));

            _reportInactiveSites.SendInactiveReports();

            _email.Verify(email => email.Send(_contacts, "2345"), Times.Exactly(1));
            _log.Verify(log => log.Add("ERROR: Unable to send inactive report email for organisation: " + "2345" + ". Error message: " + "Error sending email"));
        }
    }
}
