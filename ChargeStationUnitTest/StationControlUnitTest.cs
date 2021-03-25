using System;
using System.Collections.Generic;
using System.Text;
using ChargeStation_Handin2;
using ChargeStation_Handin2.Disp;
using ChargeStation_Handin2.DoorControl;
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

        }

        ////Test ved én rfid event, hvor telefonen tilsluttes
        //[Test]
        //public void RFIDDetected_LockerAvailable_ChargerConnected(int _id)
        //{
        //    int id = 1;
        //    _chargeControl.IsConnected().Returns(true);
        //    _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIEDEventArgs{RFID = _id});

        //    _door.Received(1).LockDoor();
        //    _chargeControl.Received(1).StartCharge();
        //    _logFile.Received(1).LogDoorLocked(id);
        //    _display.Received(1).Print("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
            
            
        //}


        ////Test ved én rfid event, hvor telefonen ikke er afsluttet
        //[Test]
        //public void RFIDDetected_LockerAvailable_ChargerNotConnected()
        //{
        //    _chargeControl.IsConnected().Returns(false);
        //    _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIEDEventArgs {RFID = 1});

        //    _display.Received(1).Print("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        //}

        [Test]
        public void DoorChangedEvent_DoorOpen()
        {
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorEventArgs{DoorState = true});
            _display.Received(1).Print("Tilslut telefon");
            
        }

        [Test]
        public void DoorChangedEvent_DoorClosed_NoPrint()
        {
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorEventArgs(){ DoorState = false });
            _display.DidNotReceive().Print("Tilslut telefon");
            _display.DidNotReceive().Print("Indlæs RFID");
        }

        [Test]
        public void DoorChangedEvent_TwoEvents_PrintMessages()
        {
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorEventArgs() { DoorState = true });

            _door.DoorStateChangedEvent += Raise.EventWith(new DoorEventArgs() { DoorState = false });
            _display.Received(1).Print("Tilslut telefon");
            _display.Received(1).Print("Indlæs RFID");
        }

    }
}
