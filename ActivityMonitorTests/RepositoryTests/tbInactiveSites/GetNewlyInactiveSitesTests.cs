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
    public class GetNewlyInactiveSitesTests
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
            
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("1234", DateTime.Today.AddDays(-3), DateTime.Today.AddDays(-3), DateTime.Today.AddDays(-1)));
        }

        [TestMethod()]
        public void GetNewlyInactiveSites_NoNewlyInactiveSitesFound_UpdatesLog()
        {
            List<String> _newlyInactiveSites = _repository.GetNewlyInactiveSites();

            Assert.AreEqual(_newlyInactiveSites.Count, 0);
            _log.Verify(log => log.Add("INFORMATION: No newly inactive sites were found."));
        }

        [TestMethod]
        public void GetNewlyInactiveSites_SingleNewlyInactiveSiteExists_CorrectDataReturned()
        {
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("2345", null, DateTime.Today, null));
            
            List<String> _newlyInactiveSites = _repository.GetNewlyInactiveSites();

            Assert.AreEqual(_newlyInactiveSites.Count, 1);
            Assert.AreEqual(_newlyInactiveSites[0].ToString(), "2345");
        }

        [TestMethod]
        public void GetNewlyInactiveSites_MultipleNewlyInactiveSiteExists_CorrectDataReturned()
        {
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("2345", null, DateTime.Today, null));
            _mockContext.tbRPT_InactiveSites.AddObject(TestHelpers.PopulateTable.AddInactiveSitesDataRow("3456", null, DateTime.Today, null));
            
            List<String> _newlyInactiveSites = _repository.GetNewlyInactiveSites();

            Assert.AreEqual(_newlyInactiveSites.Count, 2);
            Assert.AreEqual(_newlyInactiveSites[0].ToString(), "2345");
            Assert.AreEqual(_newlyInactiveSites[1].ToString(), "3456");
        }
    }
}
