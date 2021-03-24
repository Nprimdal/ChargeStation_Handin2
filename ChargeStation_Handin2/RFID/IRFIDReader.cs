using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2.RFID
{
    public interface IRFIDReader
    {
        event EventHandler<int> RFIDChangedEvent;
        void OnRfidRead(int id);

    }
}
