using System;
using ChargeStation_Handin2.Disp;

namespace ChargeStation_Handin2
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger _usbCharger;
        private IDisplay _display;
        public bool IsCharging { get; set; }


        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            IsCharging = false;
            _usbCharger = usbCharger;
            _display = display;
            _usbCharger.CurrentValueEvent += HandleCurrentEventChanged;
          
        }

        public bool IsConnected()
        {
            return _usbCharger.Connected;
        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }

        public void HandleCurrentEventChanged(object s, CurrentEventArgs e)
        {

            var current = e.Current;
            if (current > 0 && current <= 5)
            {
                if (IsCharging)
                {
                    _display.Print("Telefonen er fuldt opladet");
                    IsCharging = false;
                    _usbCharger.StopCharge();
                }
            }
            if (current > 5 && current <= 500)
            {
               _display.Print("Telefonen oplader");
               IsCharging = true;

            }
            if (current > 500)
            {
                _display.Print("Fejl - fjern straks din telefon");
                _usbCharger.StopCharge();
                IsCharging = false;
            }

        }
    }
}