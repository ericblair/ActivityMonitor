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
    public class GetOrganisationSupplierTests
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

            _mockContext.tbRPT_OrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "INPS"));
        }

        [TestMethod()]
        public void GetOrganisationSupplier_OrganisationExistsInTable_ReturnsCorrectSupplier()
        {
            string _organisation = "1234";
            string _supplier = _repository.GetOrganisationSupplier(_organisation);

            Assert.AreEqual(_supplier, "INPS");
        }

        [TestMethod]
        public void GetOrganisationSupplier_OrganisationDoesntExistInTable_UpdatesLog()
        {
            string _organisation = "test";
            string _supplier = _repository.GetOrganisationSupplier(_organisation);

            Assert.IsNull(_supplier);
            _log.Verify(log => log.Add("WARNING: No supplier was found for organisation: " + _organisation));
        }
    }
}
