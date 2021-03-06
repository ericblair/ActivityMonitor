﻿using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using Moq;
using ActivityMonitorTests.TestHelpers;

namespace ActivityMonitorTests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass()]
    public class ReportInactiveSitesTest
    {
        private IReportingEntities _mockContext;
        private Mock<ILogger> _log;
        
        private ReportInactiveSites _reportInactiveSites;
        private Mock<IEmail> _email;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new ReportingEntitiesMock();
            _log = new Mock<ILogger>();
            // _repository = new Mock<IRepository>(_log.Object, _mockContext);
            // _repository = new Mock<IRepository>();
            // _reportInactiveSites = new ReportInactiveSites(_repository.Object, _log.Object);
            _email = new Mock<IEmail>();

            // _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow())
        }

        [TestMethod]
        public void ReportInactiveSites_NumberOfInactiveSitesPerHealthBoardLimitExceeded_NoIactiveSites_LogInfo_ReturnsFalse()
        {
            // Going to need a proper repository for this test
            ActivityMonitor.Repository.Repository _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);
            _reportInactiveSites = new ReportInactiveSites(_repository, _log.Object, _email.Object);

            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("1234", DateTime.Today, DateTime.Today, null));

            bool _limitExceeded = _reportInactiveSites.NumberOfInactiveSitesPerHealthBoardLimitExceeded();

            _log.Verify(log => log.Add("No newly inactive sites"));
            Assert.IsFalse(_limitExceeded);
        }

        [TestMethod]
        public void ReportInactiveSites_NumberOfInactiveSitesPerHealthBoardLimitExceeded_LimitNotExceeded_ReturnsFalse()
        {
            // Going to need a proper repository for this test
            ActivityMonitor.Repository.Repository _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);
            _reportInactiveSites = new ReportInactiveSites(_repository, _log.Object, _email.Object);

            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("1234", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("2345", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("3456", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("4567", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("5678", null, DateTime.Today, null));

            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1234", "Highland Health Board"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("2345", "Highland Health Board"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("3456", "Highland Health Board"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("4567", "Grampian Health Board"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("5678", "Grampian Health Board"));

            bool _limitExceeded = _reportInactiveSites.NumberOfInactiveSitesPerHealthBoardLimitExceeded();

            Assert.IsFalse(_limitExceeded);
        }

        [TestMethod]
        public void ReportInactiveSites_NumberOfInactiveSitesPerHealthBoardLimitExceeded_LimitExceeded_ReturnsTrue()
        {
            // Going to need a proper repository for this test
            ActivityMonitor.Repository.Repository _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);
            _reportInactiveSites = new ReportInactiveSites(_repository, _log.Object, _email.Object);

            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("0000", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("1111", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("2222", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("3333", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("4444", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("5555", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("6666", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("7777", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("8888", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("9999", null, DateTime.Today, null));

            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("0000", "Highland Health Board"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1111", "Highland Health Board"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("2222", "Highland Health Board"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("3333", "Highland Health Board"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("4444", "Highland Health Board"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("5555", "Highland Health Board"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("6666", "Highland Health Board"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("7777", "Highland Health Board"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("8888", "Highland Health Board"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("9999", "Highland Health Board"));

            bool _limitExceeded = _reportInactiveSites.NumberOfInactiveSitesPerHealthBoardLimitExceeded();

            _log.Verify(log => log.Add("WARNING: Healthboard limit exceeded for : Highland Health Board : number of inactive sites: 10"));
            Assert.IsTrue(_limitExceeded);
        }

        [TestMethod()]
        public void ReportInactiveSites_NoInactiveOrganisations_NoReportsSent()
        {
            Mock<IRepository> _repository = new Mock<IRepository>();
            _reportInactiveSites = new ReportInactiveSites(_repository.Object, _log.Object, _email.Object);

            _repository.Setup(rep => rep.GetNewlyInactiveSites()).Returns(new List<String>());

            _reportInactiveSites.SendInactiveReports();
             
            _email.Verify(email => email.Send(It.IsAny<List<string>>(), It.IsAny<string>()), Times.Never());
            _log.Verify(log => log.Add("INFO: There are no newly inactive sites to send reports to."));
        }

        [TestMethod]
        public void ReportInactiveSites_InactiveOrgHasNoContacts_NoReportsSent()
        {
            // Going to need a proper repository for this test
            ActivityMonitor.Repository.Repository _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);
            _reportInactiveSites = new ReportInactiveSites(_repository, _log.Object, _email.Object);

            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("1234", null, DateTime.Today, null));
            List<String> _emptyList = new List<string>();  

            _reportInactiveSites.SendInactiveReports();
            _email.Verify(email => email.Send(_emptyList, It.IsAny<string>()), Times.Never());
            _log.Verify(log => log.Add("WARNING: No contacts for organisation: " + "1234" + " could be found."));
        }

        [TestMethod]
        public void ReportInactiveSites_MultipleInactiveOrgs_OneOrgHasNoContacts_EmailSentToOrgWithContacts()
        {
            ActivityMonitor.Repository.Repository _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);
            _reportInactiveSites = new ReportInactiveSites(_repository, _log.Object, _email.Object);

            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("1234", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("2345", null, DateTime.Today, null));

            _mockContext.tbRPT_OrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("2345", "INPS"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("2345", "Highland Health Board"));

            _mockContext.tbRPT_HealthBoardContacts.AddObject(TestHelpers.PopulateTable.AddHealthBoardContactsDataRow(1, "Highland Health Board", "high@land.com"));
            _mockContext.tbRPT_SupplierContacts.AddObject(TestHelpers.PopulateTable.AddSupplierContactsDataRow(1, "INPS", "test@INPS.com"));

            List<String> _supplierContact = new List<string>();
            _supplierContact.Add("test@INPS.com");
            List<String> _healthBoardContact = new List<string>();
            _healthBoardContact.Add("high@land.com");

            _reportInactiveSites.SendInactiveReports();
            _log.Verify(log => log.Add("WARNING: No contacts for organisation: " + "1234" + " could be found."));

            _email.Verify(email => email.Send(_supplierContact, _healthBoardContact, "2345"), Times.Exactly(1));
            _log.Verify(log => log.Add("Inactive email report was sent for site: " + "2345"));
            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(1).DateEmailSent, DateTime.Today);
        }

        [TestMethod]
        public void ReportInactiveSites_ErrorRaisedWhenSendingEmail_ErrorCaughtAndLogged()
        {
            ActivityMonitor.Repository.Repository _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);
            _reportInactiveSites = new ReportInactiveSites(_repository, _log.Object, _email.Object);

            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("2345", null, DateTime.Today, null));

            _mockContext.tbRPT_OrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("2345", "INPS"));
            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("2345", "Highland Health Board"));

            _mockContext.tbRPT_HealthBoardContacts.AddObject(TestHelpers.PopulateTable.AddHealthBoardContactsDataRow(1, "Highland Health Board", "high@land.com"));
            _mockContext.tbRPT_SupplierContacts.AddObject(TestHelpers.PopulateTable.AddSupplierContactsDataRow(1, "INPS", "test@INPS.com"));

            List<String> _supplierContact = new List<string>();
            _supplierContact.Add("test@INPS.com");
            List<String> _healthBoardContact = new List<string>();
            _healthBoardContact.Add("high@land.com");

            _email.Setup(email => email.Send(_supplierContact, _healthBoardContact, "2345")).Throws(new Exception("Error sending email"));

            _reportInactiveSites.SendInactiveReports();

            _email.Verify(email => email.Send(_supplierContact, _healthBoardContact, "2345"), Times.Exactly(1));
            _log.Verify(log => log.Add("ERROR: Unable to send inactive report email for organisation: " + "2345" + ". Error message: " + "Error sending email"));
        }
    }
}
