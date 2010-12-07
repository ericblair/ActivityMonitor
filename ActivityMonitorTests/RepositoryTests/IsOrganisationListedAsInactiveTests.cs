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
        private ActivityMonitor.Repository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new EPMS_StatisticsEntitiesMock();
            _log = new Mock<ILogger>();
            _repository = new ActivityMonitor.Repository(_log.Object, _mockContext);
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

            _mockContext.tbInactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow(_organisation, null, DateTime.Today, null));

            Assert.IsTrue(_repository.IsOrganisationListedAsInactive(_organisation));
        }
    }
}
