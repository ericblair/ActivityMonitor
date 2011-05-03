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
    public class SaveNewlyInactiveOrganisationTests
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

        // Update regarding commented-out code below. I can't test this properly from this level. I need to investigate

        // Not 100% sure that this is actually testing anything........
        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        //public void SaveNewlyInactiveOrganisation_ErrorOccurs_ExceptionCaught()
        //{
        //    string _organisation = "1234";

        //    Mock<IRepository> _repository = new Mock<IRepository>();
        //    _repository.Setup(rep => rep.SaveNewlyInactiveOrganisation(_organisation)).Throws(new Exception());

        //    _repository.Object.SaveNewlyInactiveOrganisation(_organisation);
        //}

        //[TestMethod]
        //public void SaveNewlyInactiveOrganisation_ErrorOccurs_LogUpdated()
        //{
        //    string _organisation = "1234";

        //    Mock<IRepository> _repository = new Mock<IRepository>();
        //    _repository.Setup(rep => rep.SaveNewlyInactiveOrganisation(_organisation)).Throws(new Exception());

        //    try
        //    {
        //        _repository.Object.SaveNewlyInactiveOrganisation(_organisation);
        //    }
        //    catch (Exception)
        //    {
        //        _log.Verify(log => log.Add("ERROR: Occured while trying to save new organisation to tbRPT_InactiveSites. Org: 1234"));
        //    }
        //}

        [TestMethod]
        public void SaveNewlyInactiveOrganisation_SaveNewInactiveOrg_SavesCorrectDetails()
        {
            string _organisation = "1234";

            _repository.SaveNewlyInactiveOrganisation(_organisation);

            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(0).Org, _organisation);
            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(0).DateEmailSent, null);
            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(0).DateCreated, DateTime.Today);
            Assert.AreEqual(_mockContext.tbRPT_InactiveSites.ElementAt(0).DateUpdated, null);
            _log.Verify(log => log.Add("INFO: New Org Added to tbRPT_InactiveSites : " + _organisation));
        }
    }
}
