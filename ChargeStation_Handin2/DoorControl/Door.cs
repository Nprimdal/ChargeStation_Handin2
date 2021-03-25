using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2.DoorControl
{
    public class Door : IDoor
    {
        public event EventHandler<DoorEventArgs> DoorStateChangedEvent;

        public void LockDoor()
        {
            OnDoorClosed();
        }

        public void UnlockDoor()
        {
            OnDoorOpen();
        }

        public void OnDoorOpen()
        {
            DoorStateChangedEvent?.Invoke(this, new DoorEventArgs() {DoorState = true});
        }

        //public void OnDoorClosed()
        //{
        //    DoorStateChangedEvent?.Invoke(this, new DoorEventArgs(){DoorState = false});
        //}

    }
}
