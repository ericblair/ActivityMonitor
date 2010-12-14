using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace ActivityMonitorTests
{
    [TestClass]
    public class EmailTests
    {
        private IEPMS_StatisticsEntities _mockContext;
        private Mock<ILogger> _log;
        private Mock<ActivityMonitor.Repository.Repository> _repository;
        private Mock<SmtpClient> _client;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new EPMS_StatisticsEntitiesMock();
            _log = new Mock<ILogger>();
            _repository = new Mock<ActivityMonitor.Repository.Repository>();
        }

        [TestMethod]
        public void Email_ComposeEmail_FormsExpectedMessage
    }
}
