using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChargeStation_Handin2;
using ChargeStation_Handin2.Logging;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;


namespace ChargeStationUnitTest
{
    class LogFileUnitTest
    {
        private LogFile _uut;
        private IDateTimeLog _datetime;


        [SetUp]
        public void Setup()
        {
            _datetime = Substitute.For<IDateTimeLog>();
            _uut = new LogFile("logfile.txt",_datetime);
        }

        [Test]
        public void TestLog_NotCalled()
        {
            _datetime.DidNotReceive().GetDateTime();
        }
        [Test]
        public void Test_LogDoorLocked_LockIdI44_DataTimeReceived5()
        {
            _uut.LogDoorLocked(44);
            _uut.LogDoorLocked(44);
            _uut.LogDoorLocked(44);
            _uut.LogDoorLocked(44);
            _uut.LogDoorLocked(44);


            _datetime.Received(5).GetDateTime();
        }
        [Test]
        public void Test_LogDoorUnLocked_LockIdI44_DataTimeReceived5()
        {
            _uut.LogDoorUnlocked(44);
            _uut.LogDoorUnlocked(44);
            _uut.LogDoorUnlocked(44);
            _uut.LogDoorUnlocked(44);
            _uut.LogDoorUnlocked(44);

            _datetime.Received(5).GetDateTime();
        }

        [Test]
        public void Test_LogDoorLocked_OneCall_ID44()
        {
            int id = 44;
            _uut.LogDoorLocked(id);

            var logfileLines = System.IO.File.ReadAllLines(@"logfile.txt");
            string[] input;
            List<string> text = new List<string>();

            foreach (var logfileLine in logfileLines)
            {
                input = logfileLine.Split(',');
                text.Add(input[2]);
            }

            int fileId = Convert.ToInt32(text.Last());

            Assert.That(id, Is.EqualTo(fileId));

        }

        [TestCase(1, 78, 90, 55)]
        public void LogDoorLocked_Locked4Times_DifferentID_LastIdEqualsFileId(int id1, int id2, int id3, int id4)
        {
            _uut.LogDoorLocked(id1);
            _uut.LogDoorLocked(id2);
            _uut.LogDoorLocked(id3);
            _uut.LogDoorLocked(id4);

            var logfileLines = System.IO.File.ReadAllLines(@"logfile.txt");
            string[] input;
            List<string> text = new List<string>();

            foreach (var logfileLine in logfileLines)
            {
                input = logfileLine.Split(',');
                text.Add(input[2]);
            }

            int fileId = Convert.ToInt32(text.Last());

            Assert.That(id4, Is.EqualTo(fileId));
        }

        [Test]
        public void Test_LogDoorUnlocked_OneCall_ID44()
        {
            int id = 44;
            _uut.LogDoorUnlocked(id);

            var logfileLines = System.IO.File.ReadAllLines(@"logfile.txt");
            string[] input;
            List<string> text = new List<string>();

            foreach (var logfileLine in logfileLines)
            {
                input = logfileLine.Split(',');
                text.Add(input[2]);
            }

            int fileId = Convert.ToInt32(text.Last());

            Assert.That(id, Is.EqualTo(fileId));

        }

        [TestCase(1, 78, 90,55)]
        public void LogDoorUnLocked_Unlocked4Times_DifferentID_LastIdEqualsFileId(int id1, int id2, int id3,int id4)
        {
            _uut.LogDoorUnlocked(id1);
            _uut.LogDoorUnlocked(id2);
            _uut.LogDoorUnlocked(id3);
            _uut.LogDoorUnlocked(id4);

            var logfileLines = System.IO.File.ReadAllLines(@"logfile.txt");
            string[] input;
            List<string> text = new List<string>();

            foreach (var logfileLine in logfileLines)
            {
                input = logfileLine.Split(',');
                text.Add(input[2]);
            }

            int fileId = Convert.ToInt32(text.Last());

            Assert.That(id4, Is.EqualTo(fileId));
        }


    }
}
