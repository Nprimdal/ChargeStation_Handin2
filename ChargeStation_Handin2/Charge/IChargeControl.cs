using System.Collections.Generic;
using System.Text;

namespace ChargeStation_Handin2
{
    public interface IChargeControl
    {
        bool IsConnected();
        void StartCharge();
        void StopCharge();
        void HandleCurrentEventChanged(object s, CurrentEventArgs e);

    }
}
