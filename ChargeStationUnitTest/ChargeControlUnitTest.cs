using System;
using System.Collections.Generic;
using System.Text;
using ChargeStation_Handin2;
using ChargeStation_Handin2.Disp;
using ChargeStation_Handin2.DoorControl;
using NUnit.Framework;
using NSubstitute;
using NUnit.Framework.Internal;

namespace ChargeStationUnitTest
{
    public class ChargeControlUnitTest
    {
        private IUsbCharger _usbCharger;
        private IDisplay _display;
        private ChargeControl _uut;

        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_usbCharger, _display);
        }

        //Test af Connection til USB
        [TestCase(false)]
        [TestCase(true)]
        public void Test_IsConnected_ReturnsConnectionStatus(bool connectionStatus)
        {
            _usbCharger.Connected.Returns(connectionStatus);
            Assert.That(_uut.IsConnected, Is.EqualTo(connectionStatus));
        }

        //Test af start og stop charge
        [Test]
        public void Test_ZeroCallStartCharge()
        {
            _usbCharger.DidNotReceive().StartCharge();
        }
        [Test]
        public void Test_ZeroCallStopCharge()
        {
            _usbCharger.DidNotReceive().StopCharge();
        }
        [Test]
        public void Test_StartChargeCalledOnes()
        {
            _uut.StartCharge();
            _usbCharger.Received(1).StartCharge();
        }
        [Test]
        public void Test_StopChargeCalledOnes()
        {
            _uut.StopCharge();
            _usbCharger.Received(1).StopCharge();
        }
        [Test]
        public void Test_StartChargeCalledO5Times()
        {
            _uut.StartCharge();
            _uut.StartCharge();
            _uut.StartCharge();
            _uut.StartCharge();
            _uut.StartCharge();

            _usbCharger.Received(5).StartCharge();
        }
        [Test]
        public void Test_StopChargeCalledO5Times()
        {
            _uut.StopCharge();
            _uut.StopCharge();
            _uut.StopCharge();
            _uut.StopCharge();
            _uut.StopCharge();

            _usbCharger.Received(5).StopCharge();
        }

        //Test af handleCurrentEventChanged
        [Test]
        public void HandleCurrentEventChanged_ZeroEvent_NotFullyCharged()
        {
            _display.DidNotReceive().Print("Telefonen er fuldt opladet");
        }

        
        [Test]
        public void HandleCurrentEventChanged_ZeroEvent_NotCharging()
        {
            _display.DidNotReceive().Print("Telefonen oplader");
        }

       
        [Test]
        public void HandleCurrentEventChanged_ZeroEvent_NotError()
        {
            _display.DidNotReceive().Print("Fejl - fjern straks din telefon");
        }


        [TestCase(0)] 
        [TestCase(6)]
        public void HandleCurrentEventChanged_NotFullyCharged(int current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs {Current = current});
            _display.DidNotReceive().Print("Telefonen er fuldt opladet");
        }

        [TestCase(5)] 
        [TestCase(501)]
        public void HandleCurrentEventChanged_NotCharging(int current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });
            _display.DidNotReceive().Print("Telefonen oplader");

        }
        [TestCase(500)]
        [TestCase(-1)]
        public void HandleCurrentEventChanged_NoError(int current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });
            _display.DidNotReceive().Print("Fejl - fjern straks din telefon");

        }


        [TestCase(1)]
        [TestCase(5)]
        public void HandleCurrentEventChanged_EventFullyCharged(int current)
        {
            _uut.IsCharging = true;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });
            _display.Received(1).Print("Telefonen er fuldt opladet");
        }
        [TestCase(6)]
        [TestCase(500)]
        public void HandleCurrentEventChanged_EventCharging(int current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });
            _display.Received(1).Print("Telefonen oplader");
        }
        [TestCase(501)]
        [TestCase(1000)]
        public void HandleCurrentEventChanged_EventError(int current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });
            _display.Received(1).Print("Fejl - fjern straks din telefon");
        }

        [TestCase(5, 1, 4,3)]
        public void HandleCurrentEventChanged_EventFullyChargedRepetitive(int current1, int current2, int current3, int current4)
        {
            _uut.IsCharging = true;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current1 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current2 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current3 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current4 });

            _display.Received(1).Print("Telefonen er fuldt opladet");
        }

        [TestCase(5, 79, 4,5)]
        public void HandleCurrentEventChanged_EventFullyCharged_2Calls(int current1, int current2, int current3, int current4)
        {
            _uut.IsCharging = true;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current1 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current2 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current3 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current4 });

            _display.Received(2).Print("Telefonen er fuldt opladet");
        }

        [TestCase(6, 90, 350, 500)]
        public void HandleCurrentEventChanged_EventChargingRepetitive(int current1, int current2, int current3, int current4)
        {
            _uut.IsCharging = true;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current1 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current2 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current3 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current4 });

            _display.Received(4).Print("Telefonen oplader");
        }

        [TestCase(500, 560, 6, 500)]
        public void HandleCurrentEventChanged_EventCharging_3Calls(int current1, int current2, int current3, int current4)
        {
            _uut.IsCharging = true;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current1 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current2 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current3 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current4 });

            _display.Received(3).Print("Telefonen oplader");
        }
        [TestCase(501, 1000, 5000, 550)]
        public void HandleCurrentEventChanged_EventErrorRepetitive(int current1, int current2, int current3, int current4)
        {
            _uut.IsCharging = true;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current1 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current2 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current3 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current4 });

            _display.Received(4).Print("Fejl - fjern straks din telefon");
        }

        [TestCase(501, 500, 1000, 0)]
        public void HandleCurrentEventChanged_EventError_2Calls(int current1, int current2, int current3, int current4)
        {
            _uut.IsCharging = true;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current1 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current2 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current3 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current4 });

            _display.Received(2).Print("Fejl - fjern straks din telefon");
        }

        



    }
}
