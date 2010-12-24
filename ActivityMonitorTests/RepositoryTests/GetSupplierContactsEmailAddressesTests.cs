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
    public class GetSupplierContactsEmailAddresses
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

            _mockContext.tbSupplierContacts.AddObject(TestHelpers.PopulateTable.AddSupplierContactsDataRow(1, "INPS", "testINPS1@test.com"));
            _mockContext.tbSupplierContacts.AddObject(TestHelpers.PopulateTable.AddSupplierContactsDataRow(2, "GPASS", "testGPASS1@test.co.uk"));
            _mockContext.tbSupplierContacts.AddObject(TestHelpers.PopulateTable.AddSupplierContactsDataRow(1, "INPS", "testINPS2@test.com"));
        }

        [TestMethod()]
        public void GetSupplierContactsEmailAddresses_ReturnsSingleContact()
        {
            string _supplier = "GPASS";
            List<String> _returnedContact = _repository.GetSupplierContactsEmailAddresses(_supplier);

            Assert.AreEqual(_returnedContact.Count, 1);
            Assert.AreEqual(_returnedContact[0].ToString(), "testGPASS1@test.co.uk");
        }

        [TestMethod()]
        public void GetSupplierContactsEmailAddresses_ReturnsMultipleContacts()
        {
            string _supplier = "INPS";
            List<String> _returnedContacts = _repository.GetSupplierContactsEmailAddresses(_supplier);

            Assert.AreEqual(_returnedContacts.Count, 2);
            Assert.AreEqual(_returnedContacts[0].ToString(), "testINPS1@test.com");
            Assert.AreEqual(_returnedContacts[1].ToString(), "testINPS2@test.com");
        }

        [TestMethod()]
        public void GetSupplierContactsEmailAddresses_LogsDetailsIfNoSupplierContactsAreFound()
        {
            string _supplier = "test";
            List<String> _returnedContacts = _repository.GetSupplierContactsEmailAddresses(_supplier);

            Assert.AreEqual(_returnedContacts.Count, 0);
            _log.Verify(log => log.Add("WARNING: No contacts could be found in tbSupplierContacts matching value: " + _supplier));
        }
    }
}
