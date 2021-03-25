using System;
using ChargeStation_Handin2.Disp;

namespace ChargeStation_Handin2
{
    public class ChargeControl : IChargeControl
    {
        private enum ChargingState
        {
            Charging,
            NotCharging,
            FullyCharged,
            Error

        };

        private IUsbCharger _usbCharger;
        private IDisplay _display;
        private ChargingState _state;


        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            _usbCharger = usbCharger;
            _display = display;
            _state = ChargingState.NotCharging;
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
            _state = ChargingState.NotCharging;

        }

        public void HandleCurrentEventChanged(object s, CurrentEventArgs e)
        {
            var current = e.Current;
            switch (_state)
            {
                case ChargingState.FullyCharged:
                    if (current > 0 && current <= 5)
                    {
                        _display.Print("Telefonen er fuldt opladet");
                        _state = ChargingState.FullyCharged;
                    }
                    break;
                case ChargingState.Charging:
                    if (current > 5 && current <= 500)
                    {
                        _display.Print("Telefonen oplader");
                        _state = ChargingState.Charging;
                    }
                    break;
                case ChargingState.Error:
                    if (current > 500)
                    {
                        _display.Print("Fejl - fjern straks din telefon");
                        _state = ChargingState.Error;
                    }
                    break;

            }


        }
    }
}