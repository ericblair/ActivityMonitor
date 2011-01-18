using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ActivityMonitorTests.TestHelpers;

namespace ActivityMonitorTests.RepositoryTests
{
    [TestClass]
    public class GetOrganisationMigrationDateTests
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
        public void GetOrganisationMigrationDate_DateExistsForOrg_CorrectDateIsReturned()
        {
            string _organisation = "1234";

            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234", DateTime.Today));

            DateTime? _migrationDate = _repository.GetOrganisationMigrationDate(_organisation);

            Assert.AreEqual(DateTime.Today, _migrationDate);
        }

        // Refactor this test - not sure if I want to allow nulls in this field but I don't want to make the decision right now
        [TestMethod]
        public void GetOrganisationMigrationDate_NullDateExistsForOrg_NullIsReturned()
        {
            string _organisation = "1234";

            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234"));

            DateTime? _migrationDate = _repository.GetOrganisationMigrationDate(_organisation);

            Assert.AreEqual(null, _migrationDate);
        }

        // Refactor - an error should probably be logged if the org is not found
        [TestMethod]
        public void GetOrganisationMigrationDate_OrgNotInTable_NullIsReturned()
        {
            string _organisation = "test";

            DateTime? _migrationDate = _repository.GetOrganisationMigrationDate(_organisation);

            Assert.AreEqual(null, _migrationDate);
        }
    }
}
