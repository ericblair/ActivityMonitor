using ActivityMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitorTests.TestHelpers;
using Moq;

namespace ActivityMonitorTests
{
    [TestClass]
    public class UpdateActivityDataTests
    {
        private IEPMS_StatisticsEntities _mockContext;
        private Mock<ILogger> _log;
        private UpdateActivityData _updateActivityData;
        Mock<IRepository> _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new EPMS_StatisticsEntitiesMock();
            _log = new Mock<ILogger>();
            _repository = new Mock<IRepository>();
            _updateActivityData = new UpdateActivityData(_repository.Object, _log.Object);
        }

        [TestMethod]
        public void UpdateActivityData_UpdateData_NoSuppliersToCheck_ThrowsError()
        {
            _repository.Setup(rep => rep.GetSuppliersToBeChecked()).Returns(new List<String>());

            try
            {
                _updateActivityData.UpdateData();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Unable to load any suppliers to check. Check config file.");
            }
        }

        [TestMethod]
        public void UpdateActivityData_UpdateData_NoOrganisationsToBeChecked_ThrowsError()
        {
            List<string> _suppliers = new List<string>();
            string _supplier = "TestSupplier";
            _suppliers.Add(_supplier);

            _repository.Setup(rep => rep.GetSuppliersToBeChecked()).Returns(_suppliers);
            _repository.Setup(rep => rep.GetSupplierOrganisations(_supplier)).Returns(new List<String>());
            
            try
            {
                _updateActivityData.UpdateData();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Unable to find any organisations to check for supplier: " + _supplier);
            }
        }

        [TestMethod]
        public void UpdateActivityData_UpdateOrgActivity_OrgInactive_NotAlreadyInactive_SiteAddedToInactiveSites()
        {
            string _organisation = "1234";

            _repository.Setup(rep => rep.IsOrganisationListedAsInactive(_organisation)).Returns(false);
            _repository.Setup(rep => rep.IsOrganisationActive(_organisation)).Returns(false);

            _updateActivityData.UpdateOrganisationActivity(_organisation);

            _repository.Verify(rep => rep.SaveNewlyInactiveOrganisation(_organisation), Times.Exactly(1));
            _log.Verify(log => log.Add("New inactive site: " + _organisation));
        }

        [TestMethod]
        public void UpdateActivityData_UpdateOrgActivity_OrgInactive_OrgAlreadyInactive_InactiveSiteUpdated()
        {
            string _organisation = "1234";

            _repository.Setup(rep => rep.IsOrganisationListedAsInactive(_organisation)).Returns(true);
            _repository.Setup(rep => rep.IsOrganisationActive(_organisation)).Returns(false);

            _updateActivityData.UpdateOrganisationActivity(_organisation);

            _repository.Verify(rep => rep.UpdateInactiveOrganisation(_organisation), Times.Exactly(1));
            _log.Verify(log => log.Add("Site still inactive:" + _organisation));
        }

        [TestMethod]
        public void UpdateActivityData_UpdateOrgActivity_OrgActive_OrgAlreadyInactive_SiteRemovedFromInactiveSites()
        {
            string _organisation = "1234";

            _repository.Setup(rep => rep.IsOrganisationListedAsInactive(_organisation)).Returns(true);
            _repository.Setup(rep => rep.IsOrganisationActive(_organisation)).Returns(true);

            _updateActivityData.UpdateOrganisationActivity(_organisation);

            _repository.Verify(rep => rep.MarkOrganisationAsActive(_organisation), Times.Exactly(1));
            _log.Verify(log => log.Add("Site no longer inactive: " + _organisation));
        }
    }
}
