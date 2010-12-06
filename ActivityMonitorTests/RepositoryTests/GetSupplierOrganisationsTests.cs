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
    public class GetSupplierOrganisationsTests
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
        public void GetSupplierOrganisations_NoMatchingSuppliersFound_LogsError()
        {
            string _supplier = "test";

            List<String> _organisations = _repository.GetSupplierOrganisations(_supplier);

            Assert.AreEqual(_organisations.Count, 0);
            _log.Verify(log => log.Add("WARNING: No organisations found for supplier: " + _supplier));
        }

        [TestMethod]
        public void GetSupplierOrganisations_SingleRecordFound_ReturnsCorrectOrgDetails()
        {
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "INPS"));

            string _supplier = "INPS";

            List<String> _organisations = _repository.GetSupplierOrganisations(_supplier);

            Assert.AreEqual(_organisations.Count, 1);
            Assert.AreEqual(_organisations[0].ToString(), "1234");
        }

        [TestMethod]
        public void GetSupplierOrganisations_MultipleRecordsFound_ReturnsCorrectOrgDetails()
        {
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "INPS"));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("2345", "GPASS"));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("3456", "INPS"));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("4567", "INPS"));

            string _supplier = "INPS";

            List<String> _organisations = _repository.GetSupplierOrganisations(_supplier);

            Assert.AreEqual(_organisations.Count, 3);
            Assert.AreEqual(_organisations[0].ToString(), "1234");
            Assert.AreEqual(_organisations[1].ToString(), "3456");
            Assert.AreEqual(_organisations[2].ToString(), "4567");
        }
    }
}
