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
    public class OrganisationHasSentAMSMessagesTests
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
        [ExpectedException(typeof(Exception), "WARNING: No records found in tbGPDailyActivity for organisation: test")]
        public void OrganisationHasSentAMSMessages_NoMatchesFound_LogsAndThrowsError()
        {
            string _organisation = "test";
            
            _repository.OrganisationHasSentAMSMessages(_organisation);

            _log.Verify(log => log.Add("WARNING: No records found in tbGPDailyActivity for organisation: " + _organisation));
        }

        [TestMethod]
        public void OrganisationHasSentAMSMessages_InactiveOrganisationExists_ReturnsFalse()
        {
            string _organisation = "1234";

            _mockContext.tbRPT_DailyActivityGP.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow(_organisation, 0, false));

            Assert.IsFalse(_repository.OrganisationHasSentAMSMessages(_organisation));
        }

        [TestMethod]
        public void OrganisationHasSentAMSMessages_ActiveOrganisationExists_ReturnsTrue()
        {
            string _organisation = "1234";

            _mockContext.tbRPT_DailyActivityGP.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 0, true));

            Assert.IsTrue(_repository.OrganisationHasSentAMSMessages(_organisation));
        }

        [TestMethod]
        public void OrganisationHasSentAMSMessages_MultipleEntriesExistForOrg_MostRecentActivityReturned()
        {
            string _organisation = "1234";

            _mockContext.tbRPT_DailyActivityGP.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 2, true));
            _mockContext.tbRPT_DailyActivityGP.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 1, true));
            _mockContext.tbRPT_DailyActivityGP.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 0, false));

            Assert.IsFalse(_repository.OrganisationHasSentAMSMessages(_organisation));
        }
    }
}
