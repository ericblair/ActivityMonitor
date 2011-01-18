using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ActivityMonitorTests.TestHelpers;

namespace ActivityMonitorTests.RepositoryTests
{
    [TestClass]
    public class IsOrganisationInMigratingSitesTableTests
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
        public void IsOrganisationInMigratingSitesTable_SiteDoesntExistInTable_ReturnsFalse()
        {
            string _organisation = "test";

            Assert.IsFalse(_repository.IsOrganisationInMigratingSitesTable(_organisation));
        }

        [TestMethod]
        public void IsOrganisationInMigratingSitesTable_SiteExistsInTable_ReturnsTrue()
        {
            string _organisation = "1234";

            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234"));

            Assert.IsTrue(_repository.IsOrganisationInMigratingSitesTable(_organisation));
        }
    }
}
