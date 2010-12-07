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
    public class MarkOrganisationAsActiveTests
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

            _mockContext.tbInactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("1234", null, DateTime.Today, null));
            _mockContext.tbInactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("2345", null, DateTime.Today, null));
            _mockContext.tbInactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("3456", null, DateTime.Today, null));
        }

        [TestMethod]
        public void MarkOrganisationAsActive_OrganisationNotFound_LogUpdated()
        {
            string _organisation = "test";

            _repository.MarkOrganisationAsActive(_organisation);

            _log.Verify(log => log.Add("ERROR: Tried to remove non-existant organisation from tbInactiveSites. Org: " + _organisation));
        }

        [TestMethod]
        public void MarkOrganisationAsActive_OrganisationNotFound_NoItemsDeleted()
        {
            string _organisation = "test";

            _repository.MarkOrganisationAsActive(_organisation);

            _log.Verify(log => log.Add("ERROR: Tried to remove non-existant organisation from tbInactiveSites. Org: " + _organisation));
            Assert.AreEqual(_mockContext.tbInactiveSites.Count(), 3);
        }

        [TestMethod]
        public void MarkOrganisationAsActive_MultipleOrganisationsExistInTable_CorrectItemIsDeleted()
        {
            string _organisation = "1234";

            _repository.MarkOrganisationAsActive(_organisation);

            Assert.AreEqual(_mockContext.tbInactiveSites.Count(), 2);
            Assert.AreEqual(_mockContext.tbInactiveSites.ElementAt(0).Org, "2345");
            Assert.AreEqual(_mockContext.tbInactiveSites.ElementAt(1).Org, "3456");
        }
    }
}
