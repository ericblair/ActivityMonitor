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
    public class IsOrganisationDispensingSite
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
        [ExpectedException(typeof(Exception), "WARNING: No records found in tbRPT_OrgSupplier for organisation: test")]
        public void IsOrganisationDispensingSite_OrgDoesntExist_LogsError()
        {
            string _organisation = "test";

            _repository.IsOrganisationDispensingSite(_organisation);

            _log.Verify(log => log.Add("WARNING: No records found in tbRPT_OrgSupplier for organisation: " + _organisation));
        }

        [TestMethod]
        public void IsOrganisationDispensingSite_OrgNotDispensingSite_ReturnsFalse()
        {
            string _organisation = "1234";

            _mockContext.tbRPT_OrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", false));

            bool _isDispensing = _repository.IsOrganisationDispensingSite(_organisation);

            Assert.IsFalse(_isDispensing);
        }

        [TestMethod]
        public void IsOrganisationDispensingSite_OrgIsDispensingSite_ReturnsTrue()
        {
            string _organisation = "1234";

            _mockContext.tbRPT_OrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", true));

            bool _isDispensing = _repository.IsOrganisationDispensingSite(_organisation);

            Assert.IsTrue(_isDispensing);
        }
    }
}
