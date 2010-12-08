using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using ActivityMonitorTests.TestHelpers;

namespace ActivityMonitorTests.Repository
{
    [TestClass()]
    public class ActivityTableHasBeenUpdatedTests
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

            _mockContext.tbGPdailyactivity.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("uTest1", 2));
            _mockContext.tbGPdailyactivity.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("uTest2", 2));
        }

        /// <summary>
        ///A test for ActivityTableHasBeenUpdated
        ///</summary>
        [TestMethod()]
        public void ActivityTableHasBeenUpdated_Default_SearchesForValuesInputOnPreviousDay_ReturnTrueWhenValuesExist()
        {
            // Add one more record containing yesterdays date to values populated by initialize (none of which match yesterdays date)
            _mockContext.tbGPdailyactivity.AddObject(TestHelpers.PopulateTable.AddGPDailyActivityDataRow("uTestA", 1));
            
            bool previousDateFound = _repository.ActivityTableHasBeenUpdated();

            Assert.IsTrue(previousDateFound);
        }

        [TestMethod]
        public void ActivityTableHasBeenUpdated_Default_SearchesForValuesInputOnPreviousDay_ReturnFalseWhenNoValuesExist()
        {
            // The test values loaded during initialization all have date's set older than the previous days date
            bool previousDateFound = _repository.ActivityTableHasBeenUpdated();

            Assert.IsFalse(previousDateFound);
        }

       [TestMethod]
        public void ActivityTableHasBeenUpdated_SearchForValuesInputOnSpecifiedDate_ReturnTrueWhenValuesExist()
        {
            // The test checks for date matching values loaded in setup data
            string _date = DateTime.Today.AddDays(-2).ToString();
            bool matchingDateFound = _repository.ActivityTableHasBeenUpdated(_date);

            Assert.IsTrue(matchingDateFound);
        }

        [TestMethod]
        public void ActivityTableHasBeenUpdated_SearchForValuesInputOnSpecifiedDate_ReturnFalseWhenNoValuesExist()
        {
            string _date = DateTime.Today.AddDays(-10).ToString();
            bool matchingDataFound = _repository.ActivityTableHasBeenUpdated(_date);

            Assert.IsFalse(matchingDataFound);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "A string was passed that could not be parsed to a date")]
        public void ActivityTableHasBeenUpdated_TrySearchingForInvalidDate_ThrowsError()
        {
            string _date = "*****";
            _repository.ActivityTableHasBeenUpdated(_date);
        }

        [TestMethod]
        public void ActivityTableHasBeenUpdated_TrySearchingForInvalidDate_LogsError()
        {
            string _date = "*****";

            try
            {
                _repository.ActivityTableHasBeenUpdated(_date);
                
            }
            catch 
            {
                _log.Verify(log => log.Add("String was not recognized as a valid DateTime."), Times.Exactly(1));
            }

        }
    }
}
