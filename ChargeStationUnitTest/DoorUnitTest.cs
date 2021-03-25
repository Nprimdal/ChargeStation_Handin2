using System.Security.Cryptography;
using ChargeStation_Handin2.DoorControl;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ChargeStationUnitTest
{
    public class Tests
    {
        private Door _uut;
        private DoorEventArgs _doorEvent;

        [SetUp]
        public void Setup()
        {
            _doorEvent = null;

            _uut = new Door();
            _uut.DoorStateChangedEvent += (o, args) => { _doorEvent = args; };
        }

        [Test]
        public void DoorStateChangedEvent_ZeroEvent_EventNotFired()
        {
            Assert.That(_doorEvent, Is.Null);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetDoor_DoorStateChangedEvent_EventFired(bool doorState)
        {
            _uut.SetDoor(doorState);
            Assert.That(_doorEvent, Is.Not.Null);
        }

        [Test]
        public void SetDoor_DoorOpen()
        {
            _uut.SetDoor(true);
            Assert.That(_doorEvent.DoorState, Is.EqualTo(true));
        }

        [Test]
        public void SetDoor_DoorClosed()
        {
            _uut.SetDoor(false);
            Assert.That(_doorEvent.DoorState, Is.EqualTo(false));
        }

        [Test]
        public void LockDoor_DoorLockIsTrue()
        {
            _uut.LockDoor();
            Assert.That(_uut.DoorLock, Is.EqualTo(true));
        }

        [Test]
        public void UnlockDoor_DoorLockIsFalse()
        {
            _uut.UnlockDoor();
            Assert.That(_uut.DoorLock, Is.EqualTo(false));
        }
    }
}