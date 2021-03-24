using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2.RFID
{
    class RFIEDReader : IRFIDReader
    {
        public event EventHandler<RFIEDEventArgs> RFIDChangedEvent;

        public void OnRfidRead(int id)
        {
            RFIDChangedEvent?.Invoke(this, new RFIEDEventArgs() {RFID =  id});
        }
    }
}
