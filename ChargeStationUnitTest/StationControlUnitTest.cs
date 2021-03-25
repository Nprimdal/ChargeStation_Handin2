using System;
using System.Collections.Generic;
using System.Text;
using ChargeStation_Handin2;
using ChargeStation_Handin2.Disp;
using ChargeStation_Handin2.RFID;
using NUnit.Framework;
using NSubstitute;

namespace ChargeStationUnitTest
{
    class StationControlUnitTest
    {
        private StationControl _uut;

        private IChargeControl _chargeControl;
        private IUsbCharger _usbCharger;
        private IDisplay _display;
        private IDoor _door;
        private ILogFile _logFile;
        private IRFIDReader _rfidReader;

        [SetUp]
        public void Setup()
        {
            _uut = new StationControl();

            _chargeControl = Substitute.For<IChargeControl>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _logFile = Substitute.For<ILogFile>();
            _rfidReader = Substitute.For<IRFIDReader>();

            _uut = new StationControl();

        }

        [Test]
        public void RFID_NotDetected()
        {
            //_rfidReader.OnRfidRead(0);

            
        }

        //Test ved én rfid event, hvor telefonen tilsluttes
        [Test]
        public void RFIDDetected_LockerAvailable_ChargerConnected(object o, int id)
        {
            id = 1;
            _chargeControl.IsConnected().Returns(true);
            //_rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIEDEventArgs());

            _door.Received(1).LockDoor();
            _chargeControl.Received(1).StartCharge();
            _logFile.Received(1).LogDoorLocked(id);
            _display.Received(1).Print("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
            
        }
    }
}
