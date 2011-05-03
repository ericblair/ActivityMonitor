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
    public class GetOrganisationSupplierReference
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
        public void GetOrganisationSupplierReference_OrgDoesntExist_LogsDetails()
        {
            string _organisation = "test";

            string _supplierReference = _repository.GetOrganisationSupplierReference(_organisation);

            _log.Verify(log => log.Add("WARNING: No supplier reference was found for organisation: " + _organisation));
            Assert.AreEqual(_supplierReference, "No Supplier Reference Found");
        }

        [TestMethod]
        public void GetOrganisationSupplierReference_OrgHasNoSupplierReference_LogsDetails()
        {
            string _organisation = "1234";

            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1234", "Test HealthBoard", ""));

            string _supplierReference = _repository.GetOrganisationSupplierReference(_organisation);

            _log.Verify(log => log.Add("WARNING: No supplier reference was found for organisation: " + _organisation));
            Assert.AreEqual(_supplierReference, "No Supplier Reference Found");
        }

        [TestMethod]
        public void GetOrganisationSupplierReference_OrgExisitsAndHasSupplierReference_ReturnsCorrectSupplierReference()
        {
            string _organisation = "1234";

            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1234", "Test HealthBoard", "Test Supplier Reference"));

            string _supplierReference = _repository.GetOrganisationSupplierReference(_organisation);

            Assert.AreEqual(_supplierReference, "Test Supplier Reference");
        }
    }
}
