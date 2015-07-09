# visualFirmata
A .Net library for implementing the Arduino Standard Firmata

The objective is to provide a simple serial interface to a 'standard firmata' microcontroller.

example:

using visualFirmata;
using System.Threading;

FirmataPort RemoteControl = new FirmataPort("COM3", 57600);
RemoteControl.Connect();

RemoteControl.PinMode(13,IOPinMode.OUTPUT);         //  Set pin 13 to output
RemoteControl.DigitalWrite(13,FirmataHighLow.HIGH); // turns LED on.
Thread.Sleep(1000);  // wait for 1 second
RemoteControl.DigitalWrite(13,FirmataHighLow.LOW); // turns LED off.
Thread.Sleep(1000);  // wait for 1 second
RemoteControl.DigitalWrite(13,FirmataHighLow.HIGH); // turns LED on.
Thread.Sleep(1000);  // wait for 1 second
RemoteControl.DigitalWrite(13,FirmataHighLow.LOW); // turns LED off.
Thread.Sleep(1000);  // wait for 1 second
RemoteControl.DigitalWrite(13,FirmataHighLow.HIGH); // turns LED on.
