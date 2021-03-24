using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2.RFID
{
    class RFIEDReader : IRFIDReader
    {
        public event EventHandler<int> RFIDChangedEvent;

        public void OnRfidRead(int id)
        {
            RFIDChangedEvent?.Invoke(this, id);
        }
    }
}
