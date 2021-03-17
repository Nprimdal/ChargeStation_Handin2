using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2
{
   
        public interface ILogFile
        {
            public void LogDoorLocked(int id);
            public void LogDoorUnlocked(int id);
        }
    
}
