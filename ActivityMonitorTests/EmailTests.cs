using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using ActivityMonitorTests.TestHelpers;

namespace ActivityMonitorTests
{
    [TestClass]
    public class EmailTests
    {
        private IEPMS_StatisticsEntities _mockContext;
        private Mock<ILogger> _log;
        private ActivityMonitor.Repository.Repository _repository;
        private Mock<SmtpClient> _client;
        private Email _email;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new EPMS_StatisticsEntitiesMock();
            _log = new Mock<ILogger>();
            _repository = new ActivityMonitor.Repository.Repository(_log.Object, _mockContext);
            _email = new Email(_repository, _log.Object);

            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("1234", "INPS"));
            _mockContext.tbOrgSupplier.AddObject(TestHelpers.PopulateTable.AddOrgSupplierDataRow("2345", "EMIS"));

            _mockContext.tbOrganisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("1234", "Highland Health Board", "Non-EMIS Site"));
            _mockContext.tbOrganisation.AddObject(TestHelpers.PopulateTable.AddOrganisationDataRow("2345", "Highland Health Board", "EMIS Site"));
        }

        [TestMethod]
        public void Email_CreateEmailSubject_OrganisationSupplierNotEMIS()
        {
            string _organisation = "1234";
            string _subject = _email.CreateEmailSubject(_organisation);

            Assert.AreEqual(_subject, "Inactive Organisation:" + _organisation);
        }

        [TestMethod]
        public void Email_CreateEmailSubject_OrganisationSupplierIsEMIS()
        {
            string _organisation = "2345";
            string _subject = _email.CreateEmailSubject(_organisation);

            Assert.AreEqual(_subject, "EMIS Site");
        }
    }
}
