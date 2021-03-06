﻿using ActivityMonitor;
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
    public class UpdateInactiveOrganisationTests
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
        public void UpdateInactiveOrganisation_OrganisationNotFound_LogUpdated()
        {
            string _organisation = "test";

            _repository.UpdateInactiveOrganisation(_organisation);

            _log.Verify(log => log.Add("ERROR: App attempted to update organisation not present in tbRPT_InactiveSites. Org: " + _organisation));
        }

        [TestMethod]
        public void UpdateInactiveOrganisation_OrganisationNeverPreviouslyBeenUpdated_CorrectDetailsUpdated()
        {
            string _organisation = "1234";
            DateTime threeDaysAgo = DateTime.Today.AddDays(-3);

            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow(_organisation, null, DateTime.Today, null));

            _repository.UpdateInactiveOrganisation(_organisation);

            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(0).Org, _organisation);
            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(0).DateEmailSent, null);
            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(0).DateCreated, DateTime.Today);
            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(0).DateUpdated, DateTime.Today);
        }

        [TestMethod]
        public void UpdateInactiveOrganisation_OrganisationHasBeenUpdatedPreviously_CorrectDetailsUpdated()
        {
            string _organisation = "1234";
            DateTime threeDaysAgo = DateTime.Today.AddDays(-3);

            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow(_organisation, null, DateTime.Today, DateTime.Today.AddDays(-1)));

            _repository.UpdateInactiveOrganisation(_organisation);

            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(0).Org, _organisation);
            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(0).DateEmailSent, null);
            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(0).DateCreated, DateTime.Today);
            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(0).DateUpdated, DateTime.Today);
        }
    }
}
