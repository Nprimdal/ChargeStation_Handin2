using System.Collections.Generic;
using System.IO;
using System.Text;
using ChargeStation_Handin2.Logging;

namespace ChargeStation_Handin2.Logging
{
    public class LogFile : ILogFile
    {
        private string _file;
        private IDateTime _dateTime;

        public LogFile(string file)
        {
            _dateTime = new DateTime();
            _file = file;
        }

        public void LogDoorLocked(int id)
        {
            using (var file = new StreamWriter(_file, true))
            {
                file.WriteLine(_dateTime.GetDateTime() + ": Skab låst med RFID: {0}", id);
            }
            
        }

        

        public void LogDoorUnlocked(int id)
        {
            using (var file = new StreamWriter(_file, true))
            {
                file.WriteLine(_dateTime.GetDateTime() + ": Skab låst op med RFID: {0}", id);
            }
            
        }
    }
}
