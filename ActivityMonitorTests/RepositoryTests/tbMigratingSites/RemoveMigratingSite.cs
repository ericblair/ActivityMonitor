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
    public class RemoveMigratingSiteTests
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

            _mockContext.tbRPT_MigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234", DateTime.Today));
            _mockContext.tbRPT_MigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("2345", DateTime.Today));
            _mockContext.tbRPT_MigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("3456", DateTime.Today));
        }

        [TestMethod]
        public void RemoveMigratingSite_OrganisationNotFound_LogUpdated()
        {
            string _organisation = "test";

            _repository.RemoveMigratingSite(_organisation);

            _log.Verify(log => log.Add("ERROR: Tried to remove non-existant organisation from tbRPT_MigratingSites. Org: " + _organisation));
        }

        [TestMethod]
        public void RemoveMigratingSite_OrganisationNotFound_NoItemsDeleted()
        {
            string _organisation = "test";

            _repository.RemoveMigratingSite(_organisation);

            Assert.AreEqual(_mockContext.tbRPT_MigratingSites.Count(), 3);
        }

        [TestMethod]
        public void RemoveMigratingSite_MultipleOrganisationsExistInTable_CorrectOrganisationIsDeleted()
        {
            string _organisation = "1234";

            _repository.RemoveMigratingSite(_organisation);

            Assert.AreEqual(_mockContext.tbRPT_MigratingSites.Count(), 2);
            Assert.AreEqual(_mockContext.tbRPT_MigratingSites.ElementAt(0).Organisation, "2345");
            Assert.AreEqual(_mockContext.tbRPT_MigratingSites.ElementAt(1).Organisation, "3456");
        }
    }
}
