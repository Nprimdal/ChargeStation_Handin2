using System.Collections.Generic;
using System.IO;
using System.Text;
using ChargeStation_Handin2.Logging;

namespace ChargeStation_Handin2.Logging
{
    public class LogFile : ILogFile
    {
        private string _fileName;
        private IDateTimeLog _dateTimeLog;

        public LogFile(string fileName, IDateTimeLog dateTimeLog)
        {
            _dateTimeLog = dateTimeLog;
            _fileName = fileName;
        }

        public void LogDoorLocked(int id)
        {
            using (var file = new StreamWriter(_fileName, true))
            {
                file.WriteLine(_dateTimeLog.GetDateTime() + ", Skab låst med RFID, {0}", id);
            }
            
        }

        

        public void LogDoorUnlocked(int id)
        {
            using (var file = new StreamWriter(_fileName, true))
            {
                file.WriteLine(_dateTimeLog.GetDateTime() + ", Skab låst op med RFID, {0}", id);
            }
            
        }
    }
}
