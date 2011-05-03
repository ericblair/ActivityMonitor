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
    public class GetMigratingSiteOriginalSupplierTests
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
        public void GetMigratingSiteOriginalSupplier_SiteNotFound_ReturnsNull()
        {
            string _organisation = "test";

            string _originalSupplier = "test";

            _originalSupplier = _repository.GetMigratingSiteOriginalSupplier(_organisation);

            Assert.IsNull(_originalSupplier);
        }

        [TestMethod]
        public void GetMigratingSiteOriginalSupplier_SiteNotFound_LogUpdated()
        {
            string _organisation = "test";

            _repository.GetMigratingSiteOriginalSupplier(_organisation);

            _log.Verify(log => log.Add("No original supplier value could be found for site: test"));
        }

        [TestMethod]
        public void GetMigratingSiteOriginalSupplier_SiteFound_ReturnsCorrectSupplier()
        {
            string _organisation = "1234";

            _mockContext.tbRPT_MigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234"));

            string _originalSupplier = _repository.GetMigratingSiteOriginalSupplier(_organisation);

            Assert.AreEqual(_originalSupplier, "test");
        }
    }
}
