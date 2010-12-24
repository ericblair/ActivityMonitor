using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;

namespace ActivityMonitorTests.TestHelpers
{
    public static partial class PopulateTable
    {
        public static tbMsg AddtbMsgDataRow(int msgrid, string organisation, DateTime msgdatetime, int msgType)
        {
            tbMsg row = new tbMsg
            {
                rid = msgrid,
                exId = new Guid(),
                exDatetime = msgdatetime,
                exMepRoleId = 1,
                exMsgSourceEpocRid = 11111111,
                exMsgDestinationEpocRid = 22222222,
                msgCxRid = 1,
                msgTypeRid = msgType,
                xmlSourceComplete = true,
                xmlSource = "test",
                requestMsgRid = 1,
                responseSendCount = 1,
                schemaVersion = "test",
                priorityId = 1,
                statusId = 1,
                softwareName = "test",
                softwareVersion = "test",
                softwareAuthor = "test",
                appServiceName = "test",
                appServiceVersion = 1,
                sttl = 1,
                id = new Guid(),
                stepRef = 1,
                datetime = msgdatetime,
                categoryId = 1,
                lastBlockId = new Guid(),
                bodyTypeCount = 1,
                batched = false,
                deletableOn = DateTime.Today,
                msgTxSenderType = "test",
                msgTxSenderId = organisation,
                msgTxSenderName = "test",
                auditCreatedOn = DateTime.Today,
                auditCreatedBy = "test",
                auditUpdatedOn = DateTime.Today,
                auditUpdatedBy = "test",
                deleteOn = DateTime.Today,
                deleteOnNextReview = DateTime.Today,
                deleteOnFixed = false,
                deleteFailedOn = DateTime.Today
            };

            return row;
        }
    }
}
