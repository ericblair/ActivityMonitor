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
    public class GetAllMigratingSitesTests
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
        public void GetAllMigratingSites_NoSitesExist_UpdatesLog()
        {
            _repository.GetAllMigratingSites();

            _log.Verify(log => log.Add("INFORMATION: No migrating sites were found."));
        }

        [TestMethod]
        public void GetAllMigratingSites_NoSitesExist_ReturnsEmptyList()
        {
            List<String> _migratingSites = _repository.GetAllMigratingSites();

            Assert.AreEqual(_migratingSites.Count, 0);
        }

        [TestMethod]
        public void GetAllMigratingSites_SingleSiteExists_ReturnsCorrectDetails()
        {
            string _organisation = "1234";

            _mockContext.tbRPT_MigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234"));

            List<String> _migratingSites = _repository.GetAllMigratingSites();

            Assert.AreEqual(_migratingSites.Count, 1);
            Assert.AreEqual(_migratingSites[0], _organisation);
        }

        [TestMethod]
        public void GetAllMigratingSites_MultipleSitesExists_ReturnsCorrectDetails()
        {
            string _organisation1 = "1234";
            string _organisation2 = "9999";     // Added to ensure items are returned sorted by org id
            string _organisation3 = "2345";

            _mockContext.tbRPT_MigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234"));
            _mockContext.tbRPT_MigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("9999"));
            _mockContext.tbRPT_MigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("2345"));

            List<String> _migratingSites = _repository.GetAllMigratingSites();

            Assert.AreEqual(_migratingSites.Count, 3);
            Assert.AreEqual(_migratingSites[0], _organisation1);
            Assert.AreEqual(_migratingSites[1], _organisation3);
            Assert.AreEqual(_migratingSites[2], _organisation2);
        }
    }
}
