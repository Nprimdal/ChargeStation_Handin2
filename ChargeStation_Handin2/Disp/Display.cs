using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2.Disp
{
    class Display : IDisplay
    {
        public string Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
