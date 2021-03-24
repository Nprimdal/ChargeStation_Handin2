using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2.DoorControl
{
    public class DoorEventArgs: EventArgs
    {
        public bool DoorState { get; set; }
    }
}
