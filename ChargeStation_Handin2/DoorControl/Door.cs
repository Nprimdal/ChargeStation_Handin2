using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2.DoorControl
{
    public class Door : IDoor
    {
        public event EventHandler<DoorEventArgs> DoorStateChangedEvent;

        public bool DoorOpen { get; private set; }

        public bool DoorLock { get; private set; } //Døren er låst ved true og åben ved false

        public void SetDoor(bool doorOpen)
        {
            if (doorOpen)
            {
                DoorOpen = true;
                OnDoorOpen();
            }
            else
            {
                DoorOpen = false;
                OnDoorOpen();
            }
        }

        public void LockDoor()
        {
            DoorLock = true;
            DoorOpen = false;
        }

        public void UnlockDoor()
        {
            DoorLock = false;
            DoorOpen = true;
        }

        
        public void OnDoorOpen()
        {
            DoorStateChangedEvent?.Invoke(this, new DoorEventArgs() {DoorState = DoorOpen});
        }


    }
}
