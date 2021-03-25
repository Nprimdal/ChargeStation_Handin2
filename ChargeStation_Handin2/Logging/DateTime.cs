using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2.Logging
{
    public class DateTime : IDateTime
    {
        public string GetDateTime()
        {
            return System.DateTime.Now.ToString();
        }
    }
}
