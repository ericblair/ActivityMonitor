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
    public class GetOrganisationHealthBoardTests
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

            _mockContext.tbOrganisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1234", "Highland Health Board"));
        }

        [TestMethod()]
        public void GetHealthBoardSupplier_OrganisationExistsInTable_ReturnsCorrectHealthBoard()
        {
            string _organisation = "1234";
            string _healthBoard = _repository.GetOrganisationHealthBoard(_organisation);

            Assert.AreEqual(_healthBoard, "Highland Health Board");
        }

        [TestMethod]
        public void GetHealthBoardSupplier_OrganisationDoesntExistInTable_UpdatesLog()
        {
            string _organisation = "test";
            string _healthBoard = _repository.GetOrganisationHealthBoard(_organisation);

            Assert.IsNull(_healthBoard);
            _log.Verify(log => log.Add("WARNING: No health board was found for organisation: " + _organisation));
        }
    }
}
