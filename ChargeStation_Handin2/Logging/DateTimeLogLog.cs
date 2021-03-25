using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2.Logging
{
    public class DateTimeLogLog : IDateTimeLog
    {
        public string GetDateTime()
        {
            return DateTime.Now.ToString();
        }
    }
}
