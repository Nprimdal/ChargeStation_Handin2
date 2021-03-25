using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargeStation_Handin2.Disp;
using ChargeStation_Handin2.DoorControl;
using ChargeStation_Handin2.RFID;
using ChargeStation_Handin2.Logging;

namespace ChargeStation_Handin2
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        private IRFIDReader _rfidReader;
        private IDisplay _display;
        private ILogFile _file;

        public StationControl(IChargeControl chargeControl, IDoor door, IRFIDReader RFIDreader, IDisplay display, ILogFile logFile)
        {
            _charger = chargeControl;
            _door = door;
            _rfidReader = RFIDreader;
            _display = display;
            _file = logFile;

            _state = LadeskabState.Available;
            _door.DoorStateChangedEvent += HandleDoorChangedEvent;
            _rfidReader.RFIDChangedEvent += RfidDetected;
            
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(object o, RFIEDEventArgs e)
        {
            int id = e.RFID;
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected())
                    {
                        _door.LockDoor();   
                        _charger.StartCharge();
                        _oldId = id;
                       _file.LogDoorLocked(id);

                        _display.Print("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.Print("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _file.LogDoorUnlocked(id);

                        _display.Print("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.Print("Forkert RFID tag");
                    }

                    break;
            }
        }

       
        private void HandleDoorChangedEvent(object o, DoorEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.DoorOpen:
                    if (!e.DoorState)
                    {
                        _display.Print("Indlæs RFID");
                        _state = LadeskabState.Available;
                    }
                    break;
                case LadeskabState.Available:
                    if (e.DoorState)
                    {
                        _display.Print("Tilslut telefon");
                        _state = LadeskabState.DoorOpen;
                    }
                    break;
            }
        }
    }
}
