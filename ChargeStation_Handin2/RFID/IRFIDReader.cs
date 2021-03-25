using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2.RFID
{
    public interface IRFIDReader
    {
        event EventHandler<RFIEDEventArgs> RFIDChangedEvent;
        void OnRfidRead();
        void SetRfidId(int id);

    }
}
