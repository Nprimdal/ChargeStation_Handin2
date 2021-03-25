using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChargeStation_Handin2
{
    public class LogFile : ILogFile
    {
        private string _file;

        public LogFile(string file)
        {
            _file = file;
        }

        public void LogDoorLocked(int id)
        {
            using (var file = new StreamWriter(_file, true))
            {
                file.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
            }
            
        }

        

        public void LogDoorUnlocked(int id)
        {
            using (var file = new StreamWriter(_file, true))
            {
                file.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
            }
            
        }
    }
}
