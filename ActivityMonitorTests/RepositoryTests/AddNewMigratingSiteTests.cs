using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ActivityMonitorTests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitorTests.RepositoryTests
{
    [TestClass]
    public class AddNewMigratingSiteTests
    {
        private IReportingEntities _mockContext;
        private Mock<ILogger> _log;
        private ActivityMonitor.Repository.Repository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new ReportingEntitiesMock();
            _log = new Mock<ILogger>();
            _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);
        }

        [TestMethod]
        public void AddNewMigratingSite_SaveNewMigratingSite_SavesCorrectDetails()
        {
            string _organisation = "1234";

            // Added supplier to allow call to complete
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "EMIS"));

            _repository.AddNewMigratingSite(_organisation, DateTime.Today);

            Assert.AreEqual(_mockContext.tbMigratingSites.ElementAt(0).Organisation, _organisation);
            Assert.AreEqual(_mockContext.tbMigratingSites.ElementAt(0).OriginalSupplier, "EMIS");
            Assert.AreEqual(_mockContext.tbMigratingSites.ElementAt(0).FutureSupplier, null);
            Assert.AreEqual(_mockContext.tbMigratingSites.ElementAt(0).PlannedMigrationDate, DateTime.Today);
            Assert.AreEqual(_mockContext.tbMigratingSites.ElementAt(0).DateNotificationEmailSent, null);
            Assert.AreEqual(_mockContext.tbMigratingSites.ElementAt(0).DateUpdated, null);
        }

        // Not sure how to test error conditions atm. See Repository.SaveNewlyInactiveOrganisation for a description of similar issue.
    }
}
