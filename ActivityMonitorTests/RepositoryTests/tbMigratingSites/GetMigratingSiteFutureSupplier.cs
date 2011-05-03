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
    public class GetMigratingSiteFutureSupplier
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
        public void GetMigratingSiteFutureSupplier_SiteNotFound_ReturnsNull()
        {
            string _organisation = "test";

            string _futureSupplier = "test";

            _futureSupplier = _repository.GetMigratingSiteFutureSupplier(_organisation);

            Assert.IsNull(_futureSupplier);
        }

        [TestMethod]
        public void GetMigratingSiteFutureSupplier_SiteNotFound_LogUpdated()
        {
            string _organisation = "test";

            _repository.GetMigratingSiteFutureSupplier(_organisation);

            _log.Verify(log => log.Add("No future supplier value could be found for site: test"));
        }

        [TestMethod]
        public void GetMigratingSiteFutureSupplier_SiteFound_ReturnsCorrectSupplier()
        {
            string _organisation = "1234";

            _mockContext.tbRPT_MigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234"));

            string _futureSupplier = _repository.GetMigratingSiteFutureSupplier(_organisation);

            Assert.AreEqual(_futureSupplier, "test");
        }
    }
}
