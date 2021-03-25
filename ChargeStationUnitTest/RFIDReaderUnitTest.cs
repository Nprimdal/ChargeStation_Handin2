using System;
using System.Collections.Generic;
using System.Text;
using ChargeStation_Handin2.RFID;
using ChargeStation_Handin2.DoorControl;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ChargeStationUnitTest
{
    class RFIDReaderUnitTest
    {
        private IRFIDReader _uut;
        private RFIEDEventArgs _rfidEvent;

        [SetUp]
        public void Setup()
        {
            _rfidEvent = null;

            _uut = new RFIEDReader();

            _uut.RFIDChangedEvent += (o, args) => { _rfidEvent = args; };
        }

        [Test]
        public void RFIDChangedEvent_NoIdSSet()
        {
            Assert.That(_rfidEvent, Is.Null);
        }

        [TestCase(00)]
        [TestCase(2)]
        [TestCase(100)]
        [TestCase(23)]
        [TestCase(48)]
        public void SetRfidId_IdSet_RFIDChangedEvent_EventFired(int id)
        {
            
            _uut.SetRfidId(id);
            Assert.That(_rfidEvent, Is.Not.Null);
        }

        [TestCase( 50)]
        [TestCase(40)]
        [TestCase(100)]
        [TestCase(23)]
        [TestCase(48)]
        public void SetRfidId_IDSet_CorrectID_RFIDChangedEvent(int id)
        {
            _uut.SetRfidId(id);
            Assert.That(_rfidEvent.RFID, Is.EqualTo(id));
        }




    }
}
