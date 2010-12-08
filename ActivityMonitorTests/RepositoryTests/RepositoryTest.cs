using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ActivityMonitorTests
{
    /// <summary>
    ///This is a test class for RepositoryTest and is intended
    ///to contain all RepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RepositoryTest
    {
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

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

        

        

        

        

        

        /// <summary>
        ///A test for GetOrganisationActivityDetails
        ///</summary>
        //[TestMethod()]
        //public void GetOrganisationActivityDetailsTest()
        //{
        //    ILogger log = null; // TODO: Initialize to an appropriate value
        //    Repository target = new Repository(log); // TODO: Initialize to an appropriate value
        //    string organisation = string.Empty; // TODO: Initialize to an appropriate value
        //    tbGPdailyactivity expected = null; // TODO: Initialize to an appropriate value
        //    tbGPdailyactivity actual;
        //    actual = target.GetOrganisationActivityDetails(organisation);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

       


        /// <summary>
        ///A test for IsOrganisationActive
        ///</summary>
        //[TestMethod()]
        //public void IsOrganisationActiveTest()
        //{
        //    ILogger log = null; // TODO: Initialize to an appropriate value
        //    Repository target = new Repository(log); // TODO: Initialize to an appropriate value
        //    string organisation = string.Empty; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = target.IsOrganisationActive(organisation);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for IsOrganisationListedAsInactive
        ///</summary>
        //[TestMethod()]
        //public void IsOrganisationListedAsInactiveTest()
        //{
        //    ILogger log = null; // TODO: Initialize to an appropriate value
        //    Repository target = new Repository(log); // TODO: Initialize to an appropriate value
        //    string organisation = string.Empty; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = target.IsOrganisationListedAsInactive(organisation);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for MarkOrganisationAsActive
        ///</summary>
        //[TestMethod()]
        //public void MarkOrganisationAsActiveTest()
        //{
        //    ILogger log = null; // TODO: Initialize to an appropriate value
        //    Repository target = new Repository(log); // TODO: Initialize to an appropriate value
        //    string organisation = string.Empty; // TODO: Initialize to an appropriate value
        //    target.MarkOrganisationAsActive(organisation);
        //    Assert.Inconclusive("A method that does not return a value cannot be verified.");
        //}

        /// <summary>
        ///A test for RecordDateInactiveWarningEmailWasSent
        ///</summary>
        //[TestMethod()]
        //public void RecordDateInactiveWarningEmailWasSentTest()
        //{
        //    ILogger log = null; // TODO: Initialize to an appropriate value
        //    Repository target = new Repository(log); // TODO: Initialize to an appropriate value
        //    string organisation = string.Empty; // TODO: Initialize to an appropriate value
        //    target.RecordDateInactiveWarningEmailWasSent(organisation);
        //    Assert.Inconclusive("A method that does not return a value cannot be verified.");
        //}

        /// <summary>
        ///A test for SaveNewlyInactiveOrganisation
        ///</summary>
        //[TestMethod()]
        //public void SaveNewlyInactiveOrganisationTest()
        //{
        //    ILogger log = null; // TODO: Initialize to an appropriate value
        //    Repository target = new Repository(log); // TODO: Initialize to an appropriate value
        //    string organisation = string.Empty; // TODO: Initialize to an appropriate value
        //    target.SaveNewlyInactiveOrganisation(organisation);
        //    Assert.Inconclusive("A method that does not return a value cannot be verified.");
        //}

        /// <summary>
        ///A test for UpdateInactiveOrganisation
        ///</summary>
        //[TestMethod()]
        //public void UpdateInactiveOrganisationTest()
        //{
        //    ILogger log = null; // TODO: Initialize to an appropriate value
        //    Repository target = new Repository(log); // TODO: Initialize to an appropriate value
        //    string organisation = string.Empty; // TODO: Initialize to an appropriate value
        //    target.UpdateInactiveOrganisation(organisation);
        //    Assert.Inconclusive("A method that does not return a value cannot be verified.");
        //}
    }
}
