using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ActivityMonitorTests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitorTests
{
    [TestClass]
    public class CheckMigratingSitesTests
    {
        private IReportingEntities _mockContext;
        private Mock<ILogger> _log;
        private CheckMigratingSites _checkMigratingSites;
        private Mock<IEmail> _email;
        private ActivityMonitor.Repository.Repository _repository; 

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new ReportingEntitiesMock();
            _log = new Mock<ILogger>();
            _email = new Mock<IEmail>();
            _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);
            _checkMigratingSites = new CheckMigratingSites(_repository, _log.Object, _email.Object);
        }

        [TestMethod]
        public void CheckMigratingSites_UpdateMigratingSitesTable_NoSitesInTable_LogUpdated()
        {
            _checkMigratingSites.UpdateMigratingSitesTable();

            _log.Verify(log => log.Add("No Migrating GPASS sites could be found. Abandoned Migrating site check"));
        }

        [TestMethod]
        public void CheckMigratingSites_UpdateMigratingSitesTable_OrgNotInTable_OrgAddedToTable()
        {
            string _organisation = "1234";

            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow(_organisation, "epoc", "Gpass", "MIGRATING TO EMIS ON 01/01/2011"));

            _checkMigratingSites.UpdateMigratingSitesTable();

            Assert.AreEqual(_mockContext.tbMigratingSites.Count(), 1);
            Assert.AreEqual(_mockContext.tbMigratingSites.ElementAt(0).Organisation, _organisation);
        }

        [TestMethod]
        public void CheckMigratingSites_UpdateMigratingSitesTable_OrgInMigratingSitesTableAlready_MigrationDateHasChanged_NewDateSaved()
        {
            string _organisation = "1234";

            // Site already in tbMigratingSites
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow(_organisation, DateTime.Today.AddDays(-5)));
            // Date has changed in tbOrgSupplier.reportingSupplier field
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow(_organisation, "epoc", "Gpass", "MIGRATING TO EMIS ON " + DateTime.Today));

            _checkMigratingSites.UpdateMigratingSitesTable();

            Assert.AreEqual(_mockContext.tbMigratingSites.Count(), 1);
            Assert.AreEqual(_mockContext.tbMigratingSites.ElementAt(0).Organisation, _organisation);
            Assert.AreEqual(_mockContext.tbMigratingSites.ElementAt(0).PlannedMigrationDate, DateTime.Today);
        }

        [TestMethod]
        public void CheckMigratingSites_UpdateMigratingSitesTable_OrgInMigratingSitesTableAlready_MigrationDateHasNotChanged_NoDataChanged()
        {
            string _organisation = "1234";

            // Site already in tbMigratingSites
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow(_organisation, DateTime.Today.AddDays(-5)));
            // Date has changed in tbOrgSupplier.reportingSupplier field
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow(_organisation, "epoc", "Gpass", "MIGRATING TO EMIS ON " + DateTime.Today.AddDays(-5)));

            _checkMigratingSites.UpdateMigratingSitesTable();

            Assert.AreEqual(_mockContext.tbMigratingSites.Count(), 1);
            Assert.AreEqual(_mockContext.tbMigratingSites.ElementAt(0).Organisation, _organisation);
            Assert.AreEqual(_mockContext.tbMigratingSites.ElementAt(0).PlannedMigrationDate, DateTime.Today.AddDays(-5));
        }

        [TestMethod]
        public void CheckMigratingSites_CheckForCompletedMigrations_CurrentSupplierDoesntMatchFutureSupplier_NoDataChanged()
        {
            string _organisation = "1234";

            // Set current supplier
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow(_organisation, "Gpass"));
            // Set future supplier
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow(_organisation, "originalSupplier", "EMIS"));

            _checkMigratingSites.CheckForCompletedMigrations();

            Assert.AreEqual(_mockContext.tbMigratingSites.Count(), 1);
            Assert.AreEqual(_mockContext.tbMigratingSites.ElementAt(0).Organisation, _organisation);
        }

        [TestMethod]
        public void CheckMigratingSites_CheckForCompletedMigrations_CurrentSupplierMatchesFutureSupplier_OrgRemovedFromTable()
        {
            string _organisation = "1234";

            // Set current supplier
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow(_organisation, "EMIS"));
            // Set future supplier
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow(_organisation, "GPASS", "EMIS"));

            _checkMigratingSites.CheckForCompletedMigrations();

            _log.Verify(log => log.Add("INFO: Record removed from tbMigratingSites.\n"
                             + " New supplier detected for site: " + _organisation
                             + " Original Supplier: " + "GPASS"
                             + " New Supplier: " + "EMIS"));
            Assert.AreEqual(_mockContext.tbMigratingSites.Count(), 0);
        }

        [TestMethod]
        public void CheckMigratingSites_SendNotificationEmailsForLateMigrations_NoUnnotifiedLateSitesInTable_LogUpdatedNoEmailsSent()
        {
            _checkMigratingSites.SendNotificationEmailsForLateMigrations();

            _log.Verify(log => log.Add("No un-notified sites with expired migrations were found."));
            _email.Verify(email => email.Send(It.IsAny<List<String>>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void CheckMigratingSites_SendNotificationEmailsForLateMigrations_NoContactsForSite_LogUpdatedNoEmailsSent()
        {
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234", DateTime.Today.AddDays(-5), null));

            _checkMigratingSites.SendNotificationEmailsForLateMigrations();

            _log.Verify(log => log.Add(("WARNING: No contacts for organisation: 1234 could be found.")));
            _email.Verify(email => email.Send(It.IsAny<List<String>>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void CheckMigratingSites_SendNotificationEmailsForLateMigrations_NoContactsForOneSiteButOtherSitesAreOK_LogUpdatedCorrectEmailSent()
        {
            // This site has an expired migration and has not been notified but has no contacts
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234", DateTime.Today.AddDays(-5), null));
            // This site has an expired migration and has not been notified and has contacts
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("2345", DateTime.Today.AddDays(-5), null));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("2345", "INPS"));
            _mockContext.tbOrganisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("2345", "Highland Health Board"));
            _mockContext.tbHealthBoardContacts.AddObject(TestHelpers.PopulateTable.AddHealthBoardContactsDataRow(1, "Highland Health Board", "high@land.com"));
            _mockContext.tbSupplierContacts.AddObject(TestHelpers.PopulateTable.AddSupplierContactsDataRow(1, "INPS", "test@INPS.com"));

            _checkMigratingSites.SendNotificationEmailsForLateMigrations();

            _log.Verify(log => log.Add(("WARNING: No contacts for organisation: 1234 could be found.")));
            
            List<String> _contacts = new List<string>();
            _contacts.Add("test@INPS.com");
            _contacts.Add("high@land.com");
            _log.Verify(log => log.Add(("Late migration report was sent for site: 2345")));
            _email.Verify(email => email.Send(_contacts, "2345"), Times.Exactly(1));
        }

        [TestMethod]
        public void CheckMigratingSites_SendNotificationEmailsForLateMigrations_SiteHasExpiredMigrationAndNoNotificationEmailHasBeenSent_LogUpdatedCorrectEmailSent()
        {
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234", DateTime.Today.AddDays(-5), null));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "INPS"));
            _mockContext.tbOrganisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1234", "Highland Health Board"));
            _mockContext.tbHealthBoardContacts.AddObject(TestHelpers.PopulateTable.AddHealthBoardContactsDataRow(1, "Highland Health Board", "high@land.com"));
            _mockContext.tbSupplierContacts.AddObject(TestHelpers.PopulateTable.AddSupplierContactsDataRow(1, "INPS", "test@INPS.com"));

            _checkMigratingSites.SendNotificationEmailsForLateMigrations();

            List<String> _contacts = new List<string>();
            _contacts.Add("test@INPS.com");
            _contacts.Add("high@land.com");
            _log.Verify(log => log.Add(("Late migration report was sent for site: 1234")));
            _email.Verify(email => email.Send(_contacts, "1234"), Times.Exactly(1));
        }
    }
}
