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
    public class GetDateTimeOfLatestAMSMessageTests
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
        public void GetDateTimeOfLatestAMSMessage_NoDataFoundForOrg_UpdatesLog()
        {
            string _organisation = "test";

            string _latestAMSMessage = _repository.GetDateTimeOfLatestAMSMessage(_organisation);

            _log.Verify(log => log.Add("No latest AMS message could be found in tbMsg for site: test"));

            Assert.AreEqual(_latestAMSMessage, "(No AMS Messages found)");
        }

        [TestMethod]
        public void GetDateTimeOfLatestAMSMessage_SingleAMSMessageFoundForOrg_CorrectDateTimeReturned()
        {
            string _organisation = "1234";

            DateTime _messageDateTime = new DateTime(2011, 01, 01, 01, 01, 01);

            _mockContext.tbMsg.AddObject(TestHelpers.PopulateTable.AddtbMsgDataRow(1, "1234", _messageDateTime, 21));

            string _latestAMSMessage = _repository.GetDateTimeOfLatestAMSMessage(_organisation);

            Assert.AreEqual(_latestAMSMessage, "01/01/2011 01:01:01");
        }

        [TestMethod]
        public void GetDateTimeOfLatestAMSMessage_MultipleAMSMessagesFoundForOrg_MostRecentDateTimeReturned()
        {
            string _organisation = "1234";

            DateTime _oldMessageDateTime = new DateTime(2011, 01, 01, 01, 01, 01);
            DateTime _newMessageDateTime = new DateTime(2011, 02, 02, 02, 02, 02);

            _mockContext.tbMsg.AddObject(TestHelpers.PopulateTable.AddtbMsgDataRow(1, "1234", _oldMessageDateTime, 21));
            _mockContext.tbMsg.AddObject(TestHelpers.PopulateTable.AddtbMsgDataRow(2, "1234", _newMessageDateTime, 21));

            string _latestAMSMessage = _repository.GetDateTimeOfLatestAMSMessage(_organisation);

            Assert.AreEqual(_latestAMSMessage, "02/02/2011 02:02:02");
        }

        // TODO: Add test to ensure only AMS P, A & C messages are included in results
    }
}
