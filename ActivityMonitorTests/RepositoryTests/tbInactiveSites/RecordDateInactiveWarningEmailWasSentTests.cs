﻿using ActivityMonitor;
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
    public class RecordDateInactiveWarningEmailWasSentTests
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

            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("1234", null, DateTime.Today, null));
        }

        [TestMethod]
        public void RecordDateInactiveWarningEmailWasSent_OrganisationNotFound_LogsError()
        {
            string _organisation = "test";

            _repository.RecordDateInactiveWarningEmailWasSent(_organisation);

            _log.Verify(log => log.Add("ERROR: Tried to update the email was sent for a non-existant organisation from tbRPT_InactiveSites. Org: " + _organisation));
        }

        [TestMethod]
        public void RecordDateInactiveWarningEmailWasSent_OrganisationFound_SavesTodaysDate()
        {
            string _organisation = "1234";

            _repository.RecordDateInactiveWarningEmailWasSent(_organisation);

            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(0).DateEmailSent, DateTime.Today);
        }
    }
}
