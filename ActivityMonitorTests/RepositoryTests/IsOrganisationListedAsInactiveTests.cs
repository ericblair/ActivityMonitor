using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitorTests.TestHelpers;

namespace ActivityMonitorTests.RepositoryTests
{
    [TestClass]
    public class IsOrganisationListedAsInactiveTests
    {
        private IEPMS_StatisticsEntities _mockContext;
        private Mock<ILogger> _log;
        private ActivityMonitor.Repository.Repository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new EPMS_StatisticsEntitiesMock();
            _log = new Mock<ILogger>();
            _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);

            _mockContext.tbInactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("1234", null, DateTime.Today, null));
        }

        [TestMethod]
        public void IsOrganisationListedAsInactive_OrgansisationNotFound_ReturnsFalse()
        {
            string _organisation = "test";

            Assert.IsFalse(_repository.IsOrganisationListedAsInactive(_organisation));
        }

        [TestMethod]
        public void IsOrganisationListedAsInactive_OrgansisationFound_ReturnsTrue()
        {
            string _organisation = "1234";

            Assert.IsTrue(_repository.IsOrganisationListedAsInactive(_organisation));
        }
    }
}
