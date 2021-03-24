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
            OnDoorClosed(new DoorEventArgs(){DoorState = false});
        }

        public void UnlockDoor()
        {
            OnDoorOpen(new DoorEventArgs(){DoorState = true});
        }

        public void OnDoorOpen(DoorEventArgs e)
        {
            DoorStateChangedEvent?.Invoke(this,e);
        }

        public void OnDoorClosed(DoorEventArgs e)
        {
            DoorStateChangedEvent?.Invoke(this, e);
        }

    }
}
