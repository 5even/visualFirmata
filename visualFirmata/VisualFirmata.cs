using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO.Ports;

namespace visualFirmata
{

    public class FirmataPort : System.ComponentModel.Component
    {
        private int[] digitalOutputData = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private int[] digitalInputData = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private int[] analogInputData = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        private byte[] analogReadBuffer = { 0, 0 };

        private int majorVersion = 0;
        private int minorVersion = 0;

        public FirmataPort(System.ComponentModel.IContainer container)
            : base()
        {
            if (container != null)
            {
                container.Add(this);
            }
        }
        public FirmataPort()
            : base()
        {
            InitializeComponent();
        }
        public FirmataPort(string PortName, int BaudRate)
        {
            prvtSerialPort.PortName = PortName;
            prvtSerialPort.BaudRate = BaudRate;
        }

        private System.ComponentModel.IContainer components = new System.ComponentModel.Container();

        private void InitializeComponent()
        {

        }

        protected override void Dispose(Boolean disposing)
        {
            try
            {
                if (disposing)
                {
                    if (this.components != null)
                    {
                        components.Dispose();
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private static SerialPort prvtSerialPort = new SerialPort();

        public string PortName
        {
            get { return prvtSerialPort.PortName; }
            set { prvtSerialPort.PortName = value; }
        }

        public int BaudRate
        {
            get { return prvtSerialPort.BaudRate; }
            set { prvtSerialPort.BaudRate = value; }
        }

        public Parity Parity
        {
            get { return prvtSerialPort.Parity; }
            set { prvtSerialPort.Parity = value; }
        }

        public StopBits StopBits
        {
            get { return prvtSerialPort.StopBits; }
            set { prvtSerialPort.StopBits = value; }
        }

        public enum CommandBytes
        {
            AnalogIOMessage = 0xE0,
            DigitalIOMessage = 0x90,
            ReportAnalogPin = 0xC0,
            ReportDigitalPort = 0xD0,
            SysExStart = 0xF0,
            SetPinMode = 0xF4,
            SysExEnd = 0xF7,
            ProtocolVersion = 0xF9,
            SystemReset = 0xFF
        }

        public enum SysExBytes
        {
            RESERVED_COMMAND = 0x00,        // 2nd SysEx data byte is a chip-specific command (AVR, PIC, TI, etc).
            ANALOG_MAPPING_QUERY = 0x69,    // ask for mapping of analog to pin numbers
            ANALOG_MAPPING_RESPONSE = 0x6A, // reply with mapping info
            CAPABILITY_QUERY = 0x6B,        // ask for supported modes and resolution of all pins
            CAPABILITY_RESPONSE = 0x6C,     // reply with supported modes and resolution
            PIN_STATE_QUERY = 0x6D,         // ask for a pin's current mode and value
            PIN_STATE_RESPONSE = 0x6E,      // reply with a pin's current mode and value
            EXTENDED_ANALOG = 0x6F,         // analog write (PWM, Servo, etc) to any pin
            SERVO_CONFIG = 0x70,            // set max angle, minPulse, maxPulse, freq
            STRING_DATA = 0x71,             // a string message with 14-bits per char
            SHIFT_DATA = 0x75,              // shiftOut config/data message (34 bits)
            I2C_REQUEST = 0x76,             // I2C request messages from a host to an I/O board
            I2C_REPLY = 0x77,               // I2C reply messages from an I/O board to a host
            I2C_CONFIG = 0x78,              // Configure special I2C settings such as power pins and delay times
            REPORT_FIRMWARE = 0x79,         // report name and version of the firmware
            SAMPLING_INTERVAL = 0x7A,       // sampling interval
            SYSEX_NON_REALTIME = 0x7E,      // MIDI Reserved for non-realtime messages
            SYSEX_REALTIME = 0x7F           // MIDI Reserved for realtime messages
        }

        public enum DigitalPinMode
        {
            INPUT = 0,
            OUTPUT = 1,
            ANALOG = 2,
            PWM = 3,
            SERVO = 4
        }

        public enum OnOff
        {
            DISABLE = 0,
            ENABLE = 1
        }

        public enum HighLow
        {
            LOW = 0,
            HIGH = 1
        }

        public enum DigitalPorts
        {
            Pin0ToPin7 = 0,
            Pin8ToPin15 = 1,
            Pin16ToPin23 = 2,
            Pin24ToPin31 = 3,
            Pin32ToPin39 = 4,
            Pin40ToPin47 = 5,
            Pin48ToPin55 = 6,
            Pin56ToPin63 = 7,
            Pin64ToPin71 = 8,
            Pin72ToPin79 = 9,
            Pin80ToPin87 = 10,
            Pin88ToPin95 = 11,
            Pin96ToPin103 = 12,
            Pin104ToPin111 = 13,
            Pin112ToPin119 = 14,
            Pin120ToPin127 = 15
        }

        public static bool suppressErrors = false;
        public void QueryVersion() {SendByteArray(new byte[] {(byte)FirmataPort.SysExBytes.REPORT_FIRMWARE });}
        public void StartSysEx() { SendByteArray(new byte[] { (byte)FirmataPort.CommandBytes.SysExStart }); }
        public void EndSysEx() { SendByteArray(new byte[] { (byte)FirmataPort.CommandBytes.SysExEnd }); }
        public void SetPinMode(int DigitalPin, DigitalPinMode DigitalPinMode) { SendByteArray(new byte[] {(byte) FirmataPort.CommandBytes.SetPinMode, (byte) DigitalPin, (byte) DigitalPinMode});}
        public void AnalogPinReport(int AnalogPin, OnOff ReportEnable) { SendByteArray(new byte[] { (byte)((byte)FirmataPort.CommandBytes.ReportAnalogPin | AnalogPin), (byte)ReportEnable }); }
        public void DigitalPinReport(FirmataPort.DigitalPorts Pins, OnOff ReportEnable) {SendByteArray(new byte[] { (byte)(((byte)FirmataPort.CommandBytes.ReportDigitalPort) | (byte)(Pins)), (byte)ReportEnable });}
        public void DigitalWrite(int pin, HighLow value)
        {
            int portNumber = (pin >> 3) & 15;
            int adjustment = (1 << (pin & 7));
            byte[] digitalWriteBytes = { 0, 0, 0 };
            
            if(value == 0){digitalOutputData[portNumber] = digitalOutputData[portNumber] & ~(adjustment);}
            else{digitalOutputData[portNumber] = digitalOutputData[portNumber] | adjustment;}

            digitalWriteBytes[0] = (byte)((byte)CommandBytes.DigitalIOMessage | (byte)portNumber);
            digitalWriteBytes[1] = (byte)((byte)digitalOutputData[portNumber] & (byte)127);
            digitalWriteBytes[2] = (byte)((byte)digitalOutputData[portNumber] >> 7);

            SendByteArray(digitalWriteBytes);
        }
        
        private void prvtSerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
           
        }

        private static void ProcessInput()
        {
            if (prvtSerialPort.IsOpen == false) { return; }
            try 
            { 
                //TODO PROCESS INPUT
            }
        }

        private void SendByteArray(byte[] Array)
        {
            try { FirmataPort.prvtSerialPort.Write(Array, 0, Array.Count()); }
            catch (Exception Ex) { if (suppressErrors == false) { throw Ex; } }
        }

        internal SerialPort intlSerialPort
        {
            get { return prvtSerialPort; }
        }
    }

    internal class LibraryParamenters
    {
        public const int DEFAULT_BAUD_RATE = 57600;
    }

    internal class MiscFunctions
    {
        public void msWait(int NumberOfMilliseconds)
        {
            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalMilliseconds < NumberOfMilliseconds)
            {

            }
        }
    }
}
