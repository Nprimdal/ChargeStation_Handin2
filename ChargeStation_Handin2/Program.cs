﻿    using System;
    using ChargeStation_Handin2;
    using ChargeStation_Handin2.DoorControl;
    using ChargeStation_Handin2.RFID;

    class Program
    {
        static void Main(string[] args)
        {
            IDoor door = new Door();
            IRFIDReader rfidReader = new RFIEDReader();
				// Assemble your system here from all the classes

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
                        door.OnDoorClose();
                        break;

                    case 'C':
                        door.OnDoorClose();
                        break;

                    case 'R':
                        Console.WriteLine("Indtast RFID id: ");
                        string idString = Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.OnRfidRead(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }

