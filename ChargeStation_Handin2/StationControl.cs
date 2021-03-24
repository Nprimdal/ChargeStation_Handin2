using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargeStation_Handin2.Disp;
using ChargeStation_Handin2.DoorControl;
using ChargeStation_Handin2.RFID;

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
        private Display _display;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl()
        {
            _door = new Door();
            _display = new Display();
            _charger = new ChargeControl();
            _rfidReader = new RFIEDReader(); 
            _state = LadeskabState.Available;
            _door.DoorStateChangedEvent += DoorOpen;
            _door.DoorStateChangedEvent += DoorClosed;
            _rfidReader.RFIDChangedEvent += RfidDetected;

        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(object o, int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
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
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        private void DoorClosed(object o, EventArgs e)
        {

        }

        private void DoorOpen(object o, DoorEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.DoorOpen:
                    if (e.DoorState == true)
                    {
                        _display.print("Tilslut telefon");
                        _charger.IsConnected();
                        _state = LadeskabState.Locked;
                    }
                    break;
                case LadeskabState.Locked:
                    if (e.DoorState == false)
                    {
                        _display.print("Indlæs RFID");
                    }
                    break;
            }
        }
        // Her mangler de andre trigger handlere
    }
}
