using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2.RFID
{
    public class RFIEDReader : IRFIDReader
    {
        public event EventHandler<RFIEDEventArgs> RFIDChangedEvent;
        public int Id { get; set; }

        public void SetRfidId(int id)                        
        {
            Id = id;
            OnRfidRead();
        }

        public void OnRfidRead()
        {
            RFIDChangedEvent?.Invoke(this, new RFIEDEventArgs(){RFID = Id});
        }

        
    }
}
