using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;

namespace ActivityMonitorTests
{
    [TestClass()]
    public class ReportInactiveSitesTest
    {
        private IEPMS_StatisticsEntities _mockContext;
        private Mock<ILogger> _log;
        private Mock<IRepository> _repository;

        private ReportInactiveSites _reportInactiveSites;
        private Mock<IEmail> _email;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new EPMS_StatisticsEntitiesMock();
            _log = new Mock<ILogger>();
            // _repository = new Mock<IRepository>(_log.Object, _mockContext);
            _repository = new Mock<IRepository>();
            _reportInactiveSites = new ReportInactiveSites(_repository.Object, _log.Object);
            _email = new Mock<IEmail>();
        }

        [TestMethod()]
        public void SendInactiveReports_NoInactiveOrganisations_NoReportsSent()
       { 
            List<String> _emptyList = new List<string>();
            _repository.Setup(rep => rep.GetNewlyInactiveSites()).Returns(_emptyList);

            _reportInactiveSites.SendInactiveReports(_email.Object);
            _email.Verify(email => email.Send(_emptyList, It.IsAny<string>()), Times.Never());
        }
    }
}
