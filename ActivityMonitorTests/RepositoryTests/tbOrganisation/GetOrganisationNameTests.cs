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
    public partial class Repository
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
        public void GetOrganisationName_OrganisationDoesntExist_DetailsLogged()
        {
            string _organisation = "test";

            _repository.GetOrganisationName(_organisation);

            _log.Verify(log => log.Add("WARNING: No organisation name was found for organisation: " + _organisation));
        }

        [TestMethod]
        public void GetOrganisationName_OrganisationNameIsNull_DetailsLogged()
        {
            string _organisation = "1234";

            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1234"));

            _repository.GetOrganisationName(_organisation);

            _log.Verify(log => log.Add("WARNING: No organisation name was found for organisation: " + _organisation));
        }

        [TestMethod]
        public void GetOrganisationName_OrganisationExistsAndHasName_CorrectNameReturned()
        {
            string _organisation = "1234";

            _mockContext.tbEPS_Organisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1234", "testHealthBoard"));

            string _orgName = _repository.GetOrganisationName(_organisation);

            Assert.AreEqual(_orgName, "test");
        }
    }
}
