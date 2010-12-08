using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitorTests.RepositoryTests
{
    [TestClass]
    public class IsOrganisationActiveTests
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
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "WARNING: No records found in tbGPDailyActivity for organisation: test")]
        public void IsOrganisationActive_NoMatchesFound_LogsAndThrowsError()
        {
            string _organisation = "test";
            
            _repository.IsOrganisationActive(_organisation);

            _log.Verify(log => log.Add("WARNING: No records found in tbGPDailyActivity for organisation: " + _organisation));
        }

        [TestMethod]
        public void IsOrganisationActive_InactiveOrganisationExists_ReturnsFalse()
        {
            string _organisation = "1234";

            _mockContext.tbGPdailyactivity.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow(_organisation, 0, false));

            Assert.IsFalse(_repository.IsOrganisationActive(_organisation));
        }

        [TestMethod]
        public void IsOrganisationActive_ActiveOrganisationExists_ReturnsTrue()
        {
            string _organisation = "1234";

            _mockContext.tbGPdailyactivity.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 0, true));

            Assert.IsTrue(_repository.IsOrganisationActive(_organisation));
        }

        [TestMethod]
        public void IsOrganisationActive_MultipleEntriesExistForOrg_MostRecentActivityReturned()
        {
            string _organisation = "1234";

            _mockContext.tbGPdailyactivity.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 2, true));
            _mockContext.tbGPdailyactivity.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 1, true));
            _mockContext.tbGPdailyactivity.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 0, false));

            Assert.IsFalse(_repository.IsOrganisationActive(_organisation));
        }
    }
}
