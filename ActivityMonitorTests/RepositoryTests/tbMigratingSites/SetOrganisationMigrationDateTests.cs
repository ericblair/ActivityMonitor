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
    public class SetOrganisationMigrationDateTests
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
        public void SetOrganisationMigrationDate_OrganisationNotFound_LogsError()
        {
            string _organisation = "test";

            _repository.SetOrganisationMigrationDate(_organisation, DateTime.Today);

            _log.Verify(log => log.Add("ERROR: App attempted to update organisation not present in tbMigratingSites. Org: " + _organisation));
        }

        [TestMethod]
        public void SetOrganisationMigrationDate_OrganisationFound_SavesCorrectDate()
        {
            string _organisation = "1234";

            _mockContext.tbMigratingSites.AddObject(TestHelpers.PopulateTable.AddMigratingSitesDataRow("1234"));

            _repository.SetOrganisationMigrationDate(_organisation, DateTime.Today);

            Assert.AreEqual(_mockContext.tbMigratingSites.ElementAt(0).PlannedMigrationDate, DateTime.Today);
        }
    }
}
