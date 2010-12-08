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
    public class SaveNewlyInactiveOrganisationTests
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
        }

        // Possibly have to write test if I deceide to throw error!
        // Should probably test what happens when a massive (or null string is passed in...

        //public void SaveNewlyInactiveOrganisation_ErrorOccurs_LogUpdated()
        //{
        //    // Cant work out how to throw an error accessing data source. Will come back to this...
        //    // THink i'll need to use something like:
        //    // Mock<EPMS_StatisticsEntities> _testmockContext = new Mock<EPMS_StatisticsEntities>();
            
        //}

        [TestMethod]
        public void SaveNewlyInactiveOrganisation_SaveNewInactiveOrg_SavesCorrectDetails()
        {
            string _organisation = "1234";

            _repository.SaveNewlyInactiveOrganisation(_organisation);

            Assert.AreEqual(_mockContext.tbInactiveSites.ElementAt(0).Org, _organisation);
            Assert.AreEqual(_mockContext.tbInactiveSites.ElementAt(0).DateEmailSent, null);
            Assert.AreEqual(_mockContext.tbInactiveSites.ElementAt(0).DateCreated, DateTime.Today);
            Assert.AreEqual(_mockContext.tbInactiveSites.ElementAt(0).DateUpdated, null);
            _log.Verify(log => log.Add("INFO: New Org Added to tbInactiveSites : " + _organisation));
        }
    }
}
