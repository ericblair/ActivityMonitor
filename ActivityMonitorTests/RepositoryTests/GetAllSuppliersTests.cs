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
    public class GetAllSuppliersTests
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
        public void GetAllSuppliers_NoSuppliersAreFound_LogsError()
        {
            List<String> _suppliers = _repository.GetAllSuppliers();

            Assert.AreEqual(_suppliers.Count, 0);
            _log.Verify(log => log.Add("WARNING: No Suppliers found in tbOrgSupplier"));
        }

        [TestMethod]
        public void GetAllSuppliers_SingleOrgSingleSupplierFound_CorrectDetailsReturned()
        {
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "INPS")); 
            
            List<String> _suppliers = _repository.GetAllSuppliers();

            Assert.AreEqual(_suppliers.Count(), 1);
            Assert.AreEqual(_suppliers[0].ToString(), "INPS");
        }

        [TestMethod]
        public void GetAllSuppliers_TwoOrgsTwoSuppliers_CorrectDetailsReturned()
        {
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "INPS"));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("2345", "EMIS")); 

            List<String> _suppliers = _repository.GetAllSuppliers();

            Assert.AreEqual(_suppliers.Count(), 2);
            Assert.AreEqual(_suppliers[0].ToString(), "EMIS");
            Assert.AreEqual(_suppliers[1].ToString(), "INPS");
        }

        [TestMethod]
        public void GetAllSuppliers_ThreeOrgsTwoSuppliers_CorrectDetailsReturned()
        {
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "INPS"));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("2345", "EMIS"));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("3456", "INPS"));

            List<String> _suppliers = _repository.GetAllSuppliers();

            Assert.AreEqual(_suppliers.Count(), 2);
            Assert.AreEqual(_suppliers[0].ToString(), "EMIS");
            Assert.AreEqual(_suppliers[1].ToString(), "INPS");
        }

        
    }

    
}
