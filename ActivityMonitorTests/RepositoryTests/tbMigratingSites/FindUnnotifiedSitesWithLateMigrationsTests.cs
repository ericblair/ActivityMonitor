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
    public class FindUnnotifiedSitesWithLateMigrationsTests
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
        public void FindUnnotifiedSitesWithLateMigrations_NoSitesInMigratingsTable_ReturnEmptyDictionary()
        {
            Dictionary<String, DateTime> _sites = new Dictionary<string, DateTime>();

            _sites = _repository.FindUnnotifiedSitesWithLateMigrations();

            Assert.AreEqual(_sites.Count, 0);
        }

        [TestMethod]
        public void FindUnnotifiedSitesWithLateMigrations_NoSitesMatchCriteria_ReturnEmptyDictionary()
        {
            // Add sites that do not match the expected criteria (plannedMigrationDate < Today && dateEmailSent == null)
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234", DateTime.Today, null));
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("2345", DateTime.Today.AddDays(-5), DateTime.Today));

            Dictionary<String, DateTime> _sites = new Dictionary<string, DateTime>();

            _sites = _repository.FindUnnotifiedSitesWithLateMigrations();

            Assert.AreEqual(_sites.Count, 0);
        }

        [TestMethod]
        public void FindUnnotifiedSitesWithLateMigrations_SingleSiteMatchesCriteria_ReturnsCorrectDetails()
        {
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234", DateTime.Today.AddDays(-5), null));

            Dictionary<String, DateTime> _sites = new Dictionary<string, DateTime>();

            _sites = _repository.FindUnnotifiedSitesWithLateMigrations();

            Assert.AreEqual(_sites.Count, 1);
            Assert.AreEqual(_sites["1234"], DateTime.Today.AddDays(-5));
        }

        [TestMethod]
        public void FindUnnotifiedSitesWithLateMigrations_MultipleSitesMatchesCriteria_ReturnsCorrectDetails()
        {
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234", DateTime.Today.AddDays(-5), null));
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("2345", DateTime.Today.AddDays(-6), null));
            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("3456", DateTime.Today.AddDays(-7), null));

            Dictionary<String, DateTime> _sites = new Dictionary<string, DateTime>();

            _sites = _repository.FindUnnotifiedSitesWithLateMigrations();

            Assert.AreEqual(_sites.Count, 3);
            Assert.AreEqual(_sites["1234"], DateTime.Today.AddDays(-5));
            Assert.AreEqual(_sites["2345"], DateTime.Today.AddDays(-6));
            Assert.AreEqual(_sites["3456"], DateTime.Today.AddDays(-7));
        }
    }
}
