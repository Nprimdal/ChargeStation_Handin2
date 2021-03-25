    using System;
    using ChargeStation_Handin2;
    using ChargeStation_Handin2.Disp;
    using ChargeStation_Handin2.DoorControl;
    using ChargeStation_Handin2.RFID;

    class Program
    {
        static void Main(string[] args)
        {
            IDoor door = new Door();
            IRFIDReader rfidReader = new RFIEDReader();
            
            
            IDisplay display = new Display();
            IUsbCharger usbCharger = new UsbChargerSimulator();
            IChargeControl chargeControl = new ChargeControl(usbCharger, display);
      

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.SetDoor(true);
                        break;

                    case 'C':
                        door.SetDoor(false);
                        break;

                    case 'R':
                        Console.WriteLine("Indtast RFID id: ");
                        string idString = Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.OnRfidRead();
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }

