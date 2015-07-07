using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Collections.Concurrent;
using System.Reflection;

namespace visualFirmata
{
    public delegate void AnalogPinReadReceivedEventHandler(int AnalogPinNumber, int Value);
    public delegate void DigitalPinReadReceivedEventHandler(int DigitalPinNumber, FirmataHighLow Value);
    public delegate void VersionInfoReceivedEventHandler(int majorVersion, int minorVersion);
    public delegate void PinStateInfoReceivedEventHandler(int DigitalPinNumber, IOPinMode DigitalPinMode, FirmataOnOff DigitalPinState, FirmataHighLow DigitalPinValue);
    public delegate void SerialDataReceivedEventHandler(byte Data);
    public delegate void SerialDataSentEventHandler(byte[] Data);

    public delegate void DigitalPinWriteSentHandler(int DigitalPinNumber, FirmataHighLow Value);
    public delegate void DigitalPortReportCommandSentHandler(FirmataDigitalPort Port, FirmataOnOff onOff);
    public delegate void AnalogPinReportCommandSentHandler(int AnalogPinNumber, FirmataOnOff onOff);
    
    public class FirmataPort : System.ComponentModel.Component
    {
        private int[] digitalOutputData = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private int[] digitalInputData = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private int[] analogInputData = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        
        private int majorVersion = 0;
        private int minorVersion = 0;

        public FirmataPort(System.ComponentModel.IContainer container) : base()
        {
            if (container != null)
            {
                container.Add(this);
            }
            prvtSerialPort.DataReceived += prvtSerialPort_DataReceived;
        }
        public FirmataPort() : base()
        {
            InitializeComponent();
            this.BaudRate = 57600;
            prvtSerialPort.DataReceived += prvtSerialPort_DataReceived;
        }
        public FirmataPort(string PortName, int BaudRate)
        {
            prvtSerialPort.PortName = PortName;
            prvtSerialPort.BaudRate = BaudRate;
            prvtSerialPort.DataReceived += prvtSerialPort_DataReceived;
        }

        private System.ComponentModel.IContainer components = new System.ComponentModel.Container();

        private void InitializeComponent()
        {
            suppressErrors = true;
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

        private SerialPort prvtSerialPort = new SerialPort();

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
        
        public static bool suppressErrors = false;
        public void QueryVersion() {SendByteArray(new byte[] {(byte)SysExCommand.REPORT_FIRMWARE });}
        public void StartSysEx() { SendByteArray(new byte[] { (byte)FirmataMessageType.START_SYSEX }); }
        public void EndSysEx() { SendByteArray(new byte[] { (byte)FirmataMessageType.END_SYSEX }); }
        public void SetPinMode(int DigitalPin, IOPinMode DigitalPinMode) { SendByteArray(new byte[] {(byte) FirmataMessageType.SET_PIN_MODE, (byte) DigitalPin, (byte) DigitalPinMode});}
        
        public void AnalogPinReport(int AnalogPinNumber, FirmataOnOff onOff)
        {
            byte Byte0 = (byte)(((byte)FirmataMessageType.REPORT_ANALOG) | (byte)AnalogPinNumber);
            byte Byte1 = (byte)(onOff);
            SendByteArray(new byte[] { Byte0, Byte1 });
            AnalogPinReportCommandSent(AnalogPinNumber, onOff);
        }
        
        /// <summary>
        /// Enable or disable port reporting.
        /// </summary>
        /// <param name="Pins">The port number to enable or disable.</param>
        /// <param name="onOff">Enable or disable</param>
        public void DigitalPinReport(FirmataDigitalPort Pins, FirmataOnOff onOff) 
        {
            byte Byte0 = (byte)(((byte)FirmataMessageType.REPORT_DIGITAL) | (byte)(Pins));
            byte Byte1 = (byte)(onOff);
            SendByteArray(new byte[] { Byte0, Byte1 });
            DigitalPortReportCommandSent(Pins, onOff);
        }
        
        /// <summary>
        /// Enable or disable port reporting.
        /// </summary>
        /// <param name="Pin">Turn on the port reporting for the given pin (between 0 and 127)</param>
        /// <param name="onOff">Enable or disable</param>
        public void DigitalPinReport(int Pin, FirmataOnOff onOff) 
        {
            FirmataDigitalPort PinsPort = (FirmataDigitalPort)(Math.Floor((double)(Pin / 8)));
            DigitalPinReport(PinsPort, onOff);
        }
        
        /// <summary>
        /// Write to a digital pin
        /// </summary>
        /// <param name="pin">Digital Pin Number (between 0 and 127)</param>
        /// <param name="value">Value to write to the pin</param>
        public void DigitalWrite(int pin, FirmataHighLow value)
        {
            byte[] digitalWriteBytes = { 0, 0, 0 };
            int portNumber = (pin >> 3) & 15;
            int adjustment = (1 << (pin & 7));
            
            
            if(value == 0)
            {
                digitalOutputData[portNumber] = digitalOutputData[portNumber] & (~adjustment);
            }
            else
            {
                digitalOutputData[portNumber] = digitalOutputData[portNumber] | adjustment;
            }

            digitalWriteBytes[0] = (byte)((byte)FirmataMessageType.DIGITAL_MESSAGE | (byte)portNumber);
            digitalWriteBytes[1] = (byte)((byte)digitalOutputData[portNumber] & (byte)127);
            digitalWriteBytes[2] = (byte)((byte)digitalOutputData[portNumber] >> 7);

            SendByteArray(digitalWriteBytes);
            DigitalPinWriteSent(pin, value);
        }
        
        private void prvtSerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            ProcessInput();
        }

        internal  SerialReadState CurrentSerialReadState = SerialReadState.Idle;
        internal  byte CurrentCommand = 0;
        internal  byte multiByteChannel = 0;
        internal  int bytesExpected = 0;

        internal Queue<byte> ReceivedBytes = new Queue<byte>();

        internal  List<byte> InputBytes = new List<byte>();
        internal  List<byte> SysExBytes = new List<byte>();


        public event DigitalPinReadReceivedEventHandler DigitalPinReadReceived;
        public event AnalogPinReadReceivedEventHandler AnalogPinReadReceived;
        public event VersionInfoReceivedEventHandler VersionInfoReceived;
        public event PinStateInfoReceivedEventHandler PinStateInfoReceived;  // TODO Parse Pin state sysex command to raise this
        public event SerialDataReceivedEventHandler SerialDataReceived;
        public event SerialDataSentEventHandler SerialDataSent;

        public event DigitalPinWriteSentHandler DigitalPinWriteSent;
        public event DigitalPortReportCommandSentHandler DigitalPortReportCommandSent;
        public event AnalogPinReportCommandSentHandler AnalogPinReportCommandSent;

        public void Connect()
        {
            prvtSerialPort.Open();
        }
        public void Disconnect()
        {
            prvtSerialPort.Close();
        }

        public Boolean IsOpen
        {
            get { return prvtSerialPort.IsOpen;}
        }

        private bool IsMIDICommand(byte Byte) {return (Byte < 0xF0);}
        private byte ExtractMIDICommand(byte Byte) { return (byte)(Byte & 0xF0); }
        private byte ExtractMIDIChannel(byte Byte) { return (byte)(Byte & 0x0F); }
        
        private  void ProcessInput()
        {
            try 
            {
                while (prvtSerialPort.BytesToRead > 0) 
                {
                    if (prvtSerialPort.IsOpen == false) { return; }  // if serial port was closed, abandon processing routine

                    byte newByte = (byte)prvtSerialPort.ReadByte();
                    SerialDataReceived(newByte);

                    switch (CurrentSerialReadState)
                    {
                        case SerialReadState.Idle:
                            if (IsMIDICommand(newByte))
                            { 
                                CurrentCommand = ExtractMIDICommand(newByte);
                                multiByteChannel = ExtractMIDIChannel(newByte);
                            }
                            else
                            {
                                CurrentCommand = newByte;
                            }
                            switch (CurrentCommand)
                            {
                                case (byte)FirmataMessageType.ANALOG_MESSAGE:
                                case (byte)FirmataMessageType.DIGITAL_MESSAGE:
                                case (byte)FirmataMessageType.REPORT_VERSION:
                                case (byte)FirmataMessageType.SET_PIN_MODE:
                                    bytesExpected = 2;
                                    CurrentSerialReadState = SerialReadState.ReadingCommand;
                                    break;
                                case (byte)FirmataMessageType.REPORT_ANALOG:
                                    // TODO Raise event for optional handling
                                    break;
                                case (byte)FirmataMessageType.REPORT_DIGITAL:
                                    // TODO Raise event for optional handling
                                    break;
                                case (byte)FirmataMessageType.START_SYSEX:
                                    CurrentSerialReadState = SerialReadState.ReadingSysEx;
                                    SysExBytes.Clear();
                                    break;
                                case (byte)FirmataMessageType.END_SYSEX:
                                    // Should never get here.  END_SYSEX will be handled under ReadingSysex case if sysex was started
                                    break;
                                case (byte)FirmataMessageType.SYSTEM_RESET:
                                    // TODO Raise event for optional system reset handling
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case SerialReadState.ReadingCommand:
                            ReceivedBytes.Enqueue(newByte);

                            if (ReceivedBytes.Count == bytesExpected)
                            {
                               switch(CurrentCommand)
                                {
                                    case (byte)FirmataMessageType.ANALOG_MESSAGE:
                                       int FirstByte = ReceivedBytes.Dequeue();
                                       int SecondByte = ReceivedBytes.Dequeue();
                                       int Value = FirstByte + (SecondByte << 7);
                                       AnalogPinReadReceived(multiByteChannel, Value);
                                        break;
                                    case (byte)FirmataMessageType.DIGITAL_MESSAGE:
                                       int Bits0to6 = ReceivedBytes.Dequeue();
                                       int Bit7 = ReceivedBytes.Dequeue();
                                       int DigitalPortValue = Bits0to6 + (Bit7 << 7);
                                        DigitalPinReadReceived(multiByteChannel,(FirmataHighLow)(DigitalPortValue));
                                        break;
                                    case (byte)FirmataMessageType.SET_PIN_MODE:
                                        // TODO Raise event for optional handling
                                        break;
                                    case (byte)FirmataMessageType.REPORT_ANALOG:
                                        // TODO Raise event for optional handling
                                        break;
                                    case (byte)FirmataMessageType.REPORT_DIGITAL:
                                        // TODO Raise event for optional handling
                                        break;
                                    case (byte)FirmataMessageType.REPORT_VERSION:
                                        majorVersion = ReceivedBytes.Dequeue();
                                        minorVersion = ReceivedBytes.Dequeue();
                                        VersionInfoReceived(majorVersion, minorVersion);
                                        break;
                                    default:
                                        break;
                                }

                                if(ReceivedBytes.Count > 0) {ReceivedBytes.Clear();}
                                CurrentSerialReadState = SerialReadState.Idle;
                            }
                            break;
                        case SerialReadState.ReadingSysEx:
                            if((FirmataMessageType)newByte != FirmataMessageType.END_SYSEX)
                            {
                                ReceivedBytes.Enqueue(newByte);
                            }
                            else
                            {
                                //TODO Parse Sysex Messages
                                if (ReceivedBytes.Count > 0) { ReceivedBytes.Clear(); }
                                CurrentSerialReadState = SerialReadState.Idle;
                            }
                            break;
                        default: break;
                    }     
                }

            }
            catch
            {

            }
        }
              
        public void SendByteArray(byte[] Array)
        {
            try 
            { 
                this.prvtSerialPort.Write(Array, 0, Array.Count());
                SerialDataSent(Array);
            }
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
        public const string DEFAULT_PORT_NAME = "COM3";
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
