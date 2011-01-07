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
    public class IsOrganisationActiveTests
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
        public void IsOrganisationActive_OrgNotSentAMSMessages_OrgNotDispensingSite_ReturnsFalse()
        {
            string _organisation = "1234";

            _mockContext.tbDailyActivityGP.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 0, false));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", false));

            Assert.IsFalse(_repository.IsOrganisationActive(_organisation));
        }

        [TestMethod]
        public void IsOrganisationActive_OrgNotSentAMSMessages_OrgIsDispensingSite_ReturnsTrue()
        {
            string _organisation = "1234";

            _mockContext.tbDailyActivityGP.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 0, false));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", true));

            Assert.IsTrue(_repository.IsOrganisationActive(_organisation));
        }

        [TestMethod]
        public void IsOrganisationActive_OrgHasSentAMSMessages_OrgNotDispensingSite_ReturnsTrue()
        {
            string _organisation = "1234";

            _mockContext.tbDailyActivityGP.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 0, true));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", false));

            Assert.IsTrue(_repository.IsOrganisationActive(_organisation));
        }

        [TestMethod]
        public void IsOrganisationActive_OrgHasSentAMSMessages_OrgIsDispensingSite_ReturnsTrue()
        {
            string _organisation = "1234";

            _mockContext.tbDailyActivityGP.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 0, true));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", true));

            Assert.IsTrue(_repository.IsOrganisationActive(_organisation));
        }

        [TestMethod]
        public void IsOrganisationActive_OrgHasBeenActiveSincetbDailyActivityGPWasUpdated_ReturnsTrue()
        {
            // for this I've taken the setup code used for:
            // IsOrganisationActive_OrgNotSentAMSMessages_OrgNotDispensingSite_ReturnsFalse()
            // but added a record to tbMsg which has a date more recent than the date 
            // tbDailyActivityGP would have been updated

            string _organisation = "1234";

            _mockContext.tbDailyActivityGP.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("1234", 0, false));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", false));
            _mockContext.tbMsg.AddObject(TestHelpers.PopulateTable.AddtbMsgDataRow(1, "1234", DateTime.Today, 21));

            Assert.IsTrue(_repository.IsOrganisationActive(_organisation));
        }
    }
}
