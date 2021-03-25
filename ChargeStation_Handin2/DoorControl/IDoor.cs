using System;
using System.Collections.Generic;
using System.Text;
using ChargeStation_Handin2.DoorControl;

namespace ChargeStation_Handin2
{
    public interface IDoor
    {
        event EventHandler<DoorEventArgs> DoorStateChangedEvent;

        void SetDoor(bool dooropen);

        void LockDoor();

        void UnlockDoor();

        void OnDoorOpen(DoorEventArgs e);



    }
}
