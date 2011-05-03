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
    public class HasSiteSentAMSGPMessagesSinceYesterdayTests
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
        public void HasSiteSentAMSGPMessagesSinceYesterday_NoRowForOrgExists_ReturnsFalse()
        {
            string _organisation = "test";

            bool _result = _repository.HasSiteSentAMSGPMessagesSinceYesterday(_organisation);

            Assert.IsFalse(_result);
        }

        [TestMethod]
        public void HasSiteSentAMSGPMessagesSinceYesterday_LatestMsgOlderThanYesterday_ReturnsFalse()
        {
            string _organisation = "1234";

            _mockContext.tbEPS_Msg.AddObject(TestHelpers.PopulateTable.AddtbMsgDataRow(1, "1234", DateTime.Today.AddDays(-3), 21));

            bool _result = _repository.HasSiteSentAMSGPMessagesSinceYesterday(_organisation);

            Assert.IsFalse(_result);
        }

        [TestMethod]
        public void HasSiteSentAMSGPMessagesSinceYesterday_LatestMsgReceivedYesterday_ReturnsFalse()
        {
            string _organisation = "1234";

            _mockContext.tbEPS_Msg.AddObject(TestHelpers.PopulateTable.AddtbMsgDataRow(1, "1234", DateTime.Today.AddDays(-1), 21));

            bool _result = _repository.HasSiteSentAMSGPMessagesSinceYesterday(_organisation);

            Assert.IsFalse(_result);
        }

        [TestMethod]
        public void HasSiteSentAMSGPMessagesSinceYesterday_LatestMsgReceivedToday_ReturnsTrue()
        {
            string _organisation = "1234";

            _mockContext.tbEPS_Msg.AddObject(TestHelpers.PopulateTable.AddtbMsgDataRow(1, "1234", DateTime.Today, 21));

            bool _result = _repository.HasSiteSentAMSGPMessagesSinceYesterday(_organisation);

            Assert.IsTrue(_result);
        }
    }
}
