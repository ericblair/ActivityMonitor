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
    public class GetMigratingGPASSSitesTests
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
        public void GetMigratingGPASSSites_NoSitesInOrgSupplierTable_ReturnEmptyDictionary()
        {
            Dictionary<String, DateTime> _sites = new Dictionary<string, DateTime>();

            _sites = _repository.GetMigratingGPASSSites();

            Assert.AreEqual(_sites.Count, 0);
        }

        [TestMethod]
        public void GetMigratingGPASSSites_NoSitesMatchCriteria_ReturnEmptyDictionary()
        {
            // Add sites that do not match the expected criteria: (supplier == "Gpass" && reportingSupplier startsWith "MIGRATING TO EMIS ON")
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "epoc", "Gpass", "MIGRATING TO GPASS ON 01/01/2011"));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "epoc", "EMIS", "MIGRATING TO EMIS ON 01/01/2011"));

            Dictionary<String, DateTime> _sites = new Dictionary<string, DateTime>();

            _sites = _repository.GetMigratingGPASSSites();

            Assert.AreEqual(_sites.Count, 0);
        }

        [TestMethod]
        public void GetMigratingGPASSSites_SingleSiteMatchesCriteria_ReturnsCorrectDetails()
        {
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "epoc", "Gpass", "MIGRATING TO EMIS ON " + DateTime.Today));

            Dictionary<String, DateTime> _sites = new Dictionary<string, DateTime>();

            _sites = _repository.GetMigratingGPASSSites();

            Assert.AreEqual(_sites.Count, 1);
            Assert.AreEqual(_sites["1234"], DateTime.Today);
        }

        [TestMethod]
        public void GetMigratingGPASSSites_MultipleSitesMatchCriteria_ReturnsCorrectDetails()
        {
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "epoc", "Gpass", "MIGRATING TO EMIS ON " + DateTime.Today));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("2345", "epoc", "Gpass", "MIGRATING TO EMIS ON " + DateTime.Today.AddDays(-5)));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("3456", "epoc", "Gpass", "MIGRATING TO EMIS ON " + DateTime.Today.AddDays(-10)));

            Dictionary<String, DateTime> _sites = new Dictionary<string, DateTime>();

            _sites = _repository.GetMigratingGPASSSites();

            Assert.AreEqual(_sites.Count, 3);
            Assert.AreEqual(_sites["1234"], DateTime.Today);
            Assert.AreEqual(_sites["2345"], DateTime.Today.AddDays(-5));
            Assert.AreEqual(_sites["3456"], DateTime.Today.AddDays(-10));
        }

        [TestMethod]
        public void GetMigratingGPASSSites_SingleSiteMatchesCriteria_InvalidDateInReportingSupplierText_LogUpdated()
        {
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "epoc", "Gpass", "MIGRATING TO EMIS ON DATETIME_PARSING_ERROR"));

            Dictionary<String, DateTime> _sites = new Dictionary<string, DateTime>();

            _sites = _repository.GetMigratingGPASSSites();

            _log.Verify(log => log.Add("Error occured converting date for site: 1234. ReportingSupplier value: MIGRATING TO EMIS ON DATETIME_PARSING_ERROR"));
            Assert.AreEqual(_sites.Count, 0);
        }

        [TestMethod]
        public void GetMigratingGPASSSites_MultipleSitesMatchCriteria_InvalidDateInReportingSupplierTextForOneSite_OnlyValidSitesReturned()
        {
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "epoc", "Gpass", "MIGRATING TO EMIS ON " + DateTime.Today));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("2345", "epoc", "Gpass", "MIGRATING TO EMIS ON DATETIME_PARSING_ERROR"));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("3456", "epoc", "Gpass", "MIGRATING TO EMIS ON " + DateTime.Today.AddDays(-10)));

            Dictionary<String, DateTime> _sites = new Dictionary<string, DateTime>();

            _sites = _repository.GetMigratingGPASSSites();

            _log.Verify(log => log.Add("Error occured converting date for site: 2345. ReportingSupplier value: MIGRATING TO EMIS ON DATETIME_PARSING_ERROR"));
            Assert.AreEqual(_sites.Count, 2);
            Assert.AreEqual(_sites["1234"], DateTime.Today);
            Assert.AreEqual(_sites["3456"], DateTime.Today.AddDays(-10));
        }
    }
}
