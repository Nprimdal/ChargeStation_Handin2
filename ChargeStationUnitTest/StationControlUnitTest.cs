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
        private IDisplay _display;
        private IDoor _door;
        private ILogFile _logFile;
        private IRFIDReader _rfidReader;

        [SetUp]
        public void Setup()
        {
            
            _chargeControl = Substitute.For<IChargeControl>(); ;
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _logFile = Substitute.For<ILogFile>();
            _rfidReader = Substitute.For<IRFIDReader>();

            _uut = new StationControl(_chargeControl, _door, _rfidReader, _display, _logFile);

        }

        //RFID Event: Test ved én rfid event, hvor telefonen tilsluttes
        [Test]
        public void RFIDDetected_LockerAvailable_ChargerConnected()
        {
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIEDEventArgs { RFID = 1 });

            _door.Received(1).LockDoor();
            _chargeControl.Received(1).StartCharge();
            _logFile.Received(1).LogDoorLocked(1);
            _display.Received(1).Print("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }


        //RFID Event: Test ved én rfid event, hvor telefonen ikke er tilsluttet
        [Test]
        public void RFIDDetected_LockerAvailable_ChargerNotConnected()
        {
            _chargeControl.IsConnected().Returns(false);
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIEDEventArgs { RFID = 1 });

            _display.Received(1).Print("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }



        //RFID Event: Test for om ladeskabet åbner, når det registrerer ens ID
        [Test]
        public void UnlockDoor_CheckCorrectID_Correct()
        {
            
            int oldId = 5;
            int newId = 5;
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIEDEventArgs { RFID = oldId });

            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIEDEventArgs { RFID = newId });

            _logFile.Received(1).LogDoorUnlocked(newId);
            _chargeControl.Received(1).StopCharge();
            _door.Received(1).UnlockDoor();

            _display.Received(1).Print("Tag din telefon ud af skabet og luk døren");
        }

        //RFID Event: Test for om ladeskabet forbliver lukket, når det registrerer to forskellige ID
        [Test]
        public void UnlockDoor_CheckCorrectID_Wrong()
        {
            int oldId = 3;
            int newId = 5;
            _chargeControl.IsConnected().Returns(true);
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIEDEventArgs { RFID = oldId });

            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RFIEDEventArgs { RFID = newId });

            _logFile.DidNotReceive().LogDoorUnlocked(newId);
            _chargeControl.DidNotReceive().StopCharge();
            _door.DidNotReceive().UnlockDoor();

            _display.Received(1).Print("Forkert RFID tag");
        }




        //DoorEvent: Test af metoden, som håndterer door events. Hvis døren registreres åben, udskrives en meddelelse
        [Test]
        public void DoorChangedEvent_DoorOpen()
        {
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorEventArgs{DoorState = true});
            _display.Received(1).Print("Tilslut telefon");
            
        }

        //DoorEvent: Test af metoden, som håndterer door events.  Hvis døren registreres som lukket, så udskrives der ikke meddelelser
        [Test]
        public void DoorChangedEvent_DoorClosed_NoPrint()
        {
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorEventArgs(){ DoorState = false});
            _display.DidNotReceive().Print("Tilslut telefon");
            _display.DidNotReceive().Print("Indlæs RFID");
        }

        //DoorEvent: Her kaldes event hvor døren åbnes og lukkes. Døren skal åbnes før den kan lukkes
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
