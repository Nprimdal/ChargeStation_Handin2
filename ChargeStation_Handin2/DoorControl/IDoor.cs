using System;
using System.Collections.Generic;
using System.Text;
using ChargeStation_Handin2.DoorControl;

namespace ChargeStation_Handin2
{
    public interface IDoor
    {
        event EventHandler<DoorEventArgs> DoorStateChangedEvent;
        public void LockDoor();

        public void UnlockDoor();
    }
}
