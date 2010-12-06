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
    public class GetHealthBoardContactsEmailAddressesTests
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

            // Added to setup data for GetHealthBoardContactEmailsAddresses Test
            _mockContext.tbHealthBoardContacts.AddObject(TestHelpers.PopulateTable.AddHealthBoardContactsDataRow(1, "Highland Health Board", "highland1@healthboard.com"));
            _mockContext.tbHealthBoardContacts.AddObject(TestHelpers.PopulateTable.AddHealthBoardContactsDataRow(2, "Grampian Health Board", "grampian1@healthboard.com"));
            _mockContext.tbHealthBoardContacts.AddObject(TestHelpers.PopulateTable.AddHealthBoardContactsDataRow(3, "Highland Health Board", "highland2@healthboard.com"));
        }

        [TestMethod()]
        public void GetHealthBoardContactsEmailAddresses_ReturnsSingleContact()
        {
            string _healthBoard = "Grampian Health Board";
            List<String> _returnedContact = _repository.GetHealthBoardContactsEmailAddresses(_healthBoard);

            Assert.AreEqual(_returnedContact.Count, 1);
            Assert.AreEqual(_returnedContact[0].ToString(), "grampian1@healthboard.com");
        }

        [TestMethod()]
        public void GetHealthBoardContactsEmailAddresses_ReturnsMultipleContacts()
        {
            string _healthBoard = "Highland Health Board";
            List<String> _returnedContacts = _repository.GetHealthBoardContactsEmailAddresses(_healthBoard);

            Assert.AreEqual(_returnedContacts.Count, 2);
            Assert.AreEqual(_returnedContacts[0].ToString(), "highland1@healthboard.com");
            Assert.AreEqual(_returnedContacts[1].ToString(), "highland2@healthboard.com");
        }

        [TestMethod()]
        public void GetHealthBoardContactsEmailAddresses_LogsDetailsIfNoHealthBoardContactsAreFound()
        {
            string _healthBoard = "test";
            List<String> _returnedContacts = _repository.GetHealthBoardContactsEmailAddresses(_healthBoard);

            Assert.AreEqual(_returnedContacts.Count, 0);
            _log.Verify(log => log.Add("WARNING: No contacts could be found in tbHealthBoardContacts matching value: " + _healthBoard));
        }
    }
}
