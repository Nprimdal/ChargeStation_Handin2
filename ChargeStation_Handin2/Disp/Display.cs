using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2.Disp
{
    public class Display : IDisplay
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
