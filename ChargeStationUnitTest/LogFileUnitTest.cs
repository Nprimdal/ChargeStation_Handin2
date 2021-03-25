using System;
using System.Collections.Generic;
using System.Text;
using ChargeStation_Handin2;
using ChargeStation_Handin2.Logging;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using DateTime = System.DateTime;

namespace ChargeStationUnitTest
{
    class LogFileUnitTest
    {
        private LogFile _uut;
        private IDateTime _datetime;


        [SetUp]
        public void Setup()
        {
            _datetime = Substitute.For<IDateTime>();
            _uut = new LogFile("logfile.txt");

        }

        [Test]
        public void TestLog_NotCalled()
        {
            _datetime.DidNotReceive().GetDateTime();
        }

        [Test]
        public void Test_LogDoorLocked_OneCall_ID44()
        {
            int id = 44;
            _uut.LogDoorLocked(id);


        }

    }
}
