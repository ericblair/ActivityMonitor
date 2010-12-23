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
    public class IsOrganisationActive
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
        public void IsOrganisationActive_OrgNotSentAMSMessages_OrgNotDispensingSite_ReturnsFalse()
        {
            string _organisation = "1234";

            _mockContext.tbGPdailyactivity.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 0, false));
            _mockContext.tbOrganisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1234", false));

            Assert.IsFalse(_repository.IsOrganisationActive(_organisation));
        }

        [TestMethod]
        public void IsOrganisationActive_OrgNotSentAMSMessages_OrgIsDispensingSite_ReturnsTrue()
        {
            string _organisation = "1234";

            _mockContext.tbGPdailyactivity.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 0, false));
            _mockContext.tbOrganisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1234", true));

            Assert.IsTrue(_repository.IsOrganisationActive(_organisation));
        }

        [TestMethod]
        public void IsOrganisationActive_OrgHasSentAMSMessages_OrgNotDispensingSite_ReturnsTrue()
        {
            string _organisation = "1234";

            _mockContext.tbGPdailyactivity.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 0, true));
            _mockContext.tbOrganisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1234", false));

            Assert.IsTrue(_repository.IsOrganisationActive(_organisation));
        }

        [TestMethod]
        public void IsOrganisationActive_OrgHasSentAMSMessages_OrgIsDispensingSite_ReturnsTrue()
        {
            string _organisation = "1234";

            _mockContext.tbGPdailyactivity.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 0, true));
            _mockContext.tbOrganisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1234", true));

            Assert.IsTrue(_repository.IsOrganisationActive(_organisation));
        }
    }
}
