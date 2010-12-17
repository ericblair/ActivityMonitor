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
    public class GetOrganisationLatestDate
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
        public void GetOrganisationLatestDate_OrgDoesntExist_LogsDetails()
        {
            string _organisation = "test";

            string _latestDate = _repository.GetOrganisationLatestMessageDate(_organisation);

            _log.Verify(log => log.Add("No latest AMS value found for: " + _organisation));
            Assert.AreEqual(_latestDate, "Could not retrieve date");
        }

        [TestMethod]
        public void GetOrganisationLatestDate_OrgsLatestDateIsNull_LogsDetails()
        {
            string _organisation = "1234";

            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "Test Supplier", null));

            string _latestDate = _repository.GetOrganisationLatestMessageDate(_organisation);

            _log.Verify(log => log.Add("No latest AMS value found for: " + _organisation));
            Assert.AreEqual(_latestDate, "Could not retrieve date");
        }

        [TestMethod]
        public void GetOrganisationLatestDate_OrgsExistsAndHasLatestAMSDate_ReturnsCorrectDate()
        {
            string _organisation = "1234";

            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "Test Supplier", DateTime.Today.AddDays(-1)));

            string _latestDate = _repository.GetOrganisationLatestMessageDate(_organisation);

            Assert.AreEqual(_latestDate, DateTime.Today.AddDays(-1).ToShortDateString());
        }
    }
}
