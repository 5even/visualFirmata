using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using visualFirmata;
using System.Threading;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public event EventHandler FormBaudRateChanged;
        public int FormBaudRate 
        {
            get{return prvtFirmataPort.BaudRate;}
            set { 
                if(prvtFirmataPort.BaudRate != value)
                {
                    prvtFirmataPort.BaudRate = value;
                    EventHandler handler = FormBaudRateChanged;
                    if (handler != null) handler(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler FormPortNameChanged;
        public string FormPortName
        {
            get { return prvtFirmataPort.PortName; }
            set
            {
                if(prvtFirmataPort.PortName != value)
                {
                    prvtFirmataPort.PortName = value;
                    EventHandler handler = FormPortNameChanged;
                    if (handler != null) handler(this, EventArgs.Empty);
                }
            }
        }

        private const string DISCONNECT = "disconnect";
        private const string CONNECT = "connect";

        private FirmataPort prvtFirmataPort = new FirmataPort();

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (connectButton.Text != DISCONNECT || !prvtFirmataPort.IsOpen)
            {

                prvtFirmataPort.PortName = portNameComboBox.Items[portNameComboBox.SelectedIndex].ToString();
                prvtFirmataPort.BaudRate = Convert.ToInt32(baudRateTextBox.Text);
                prvtFirmataPort.Connect();
                connectButton.Text = DISCONNECT;
                incomingDataTextBox.AppendText( "Connected to " + prvtFirmataPort.PortName + " with baudrate " + prvtFirmataPort.BaudRate.ToString() + Environment.NewLine);
                incomingDataTextBox.SelectionStart = incomingDataTextBox.Text.Length;
                incomingDataTextBox.ScrollToCaret();
                baudRateTextBox.Enabled = false;
                BlinkLED();

            }
            else
            {
                if (prvtFirmataPort.IsOpen) { prvtFirmataPort.Disconnect(); }
                connectButton.Text = CONNECT;
                baudRateTextBox.Enabled = true;
                incomingDataTextBox.AppendText(Environment.NewLine + "Disconnected from " + prvtFirmataPort.PortName + " with baudrate " + prvtFirmataPort.BaudRate.ToString() + Environment.NewLine);
                incomingDataTextBox.SelectionStart = incomingDataTextBox.Text.Length;
                incomingDataTextBox.ScrollToCaret();
            }
        }

        private void BlinkLED(int msCount = 1000)
        {
            ThreadPool.QueueUserWorkItem(o => sendBlinks(msCount));
        }
        private void sendBlinks(int msCount = 1000)
        {
            prvtFirmataPort.SetPinMode(13, IOPinMode.OUTPUT);
            System.Threading.Thread.Sleep(10);
            prvtFirmataPort.DigitalWrite(13, FirmataHighLow.HIGH);
            System.Threading.Thread.Sleep(msCount);
            prvtFirmataPort.DigitalWrite(13, FirmataHighLow.LOW);
            System.Threading.Thread.Sleep(msCount);
            prvtFirmataPort.DigitalWrite(13, FirmataHighLow.HIGH);
            System.Threading.Thread.Sleep(msCount);
            prvtFirmataPort.DigitalWrite(13, FirmataHighLow.LOW);
            System.Threading.Thread.Sleep(msCount);
            prvtFirmataPort.DigitalWrite(13, FirmataHighLow.HIGH);
            System.Threading.Thread.Sleep(msCount);
            prvtFirmataPort.DigitalWrite(13, FirmataHighLow.LOW);
            System.Threading.Thread.Sleep(msCount);
            prvtFirmataPort.DigitalWrite(13, FirmataHighLow.HIGH);
            System.Threading.Thread.Sleep(msCount);
            prvtFirmataPort.DigitalWrite(13, FirmataHighLow.LOW);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sendAsComboBox.SelectedIndex = 0;
            baudRateTextBox.DataBindings.Add("Text", this, "FormBaudRate");
            portNameComboBox.DataBindings.Add("Text", this, "FormPortName");
            UpdatePortNameComboBoxItems();
            prvtFirmataPort.DigitalPinReadReceived += DigitalPinRcvd;
            prvtFirmataPort.SerialDataReceived += SerialDataReceived;
            prvtFirmataPort.SerialDataSent += SerialDataSent;

            outgoingDataToolStripMenuItem.DropDownItemClicked += SetDataFormat;
            incomingDataToolStripMenuItem.DropDownItemClicked += SetDataFormat;
            baseToolStripMenuItem.DropDownItemClicked += SetDataFormat;

            foreach( string formatName in Enum.GetNames(typeof(PrintDataFormat)))
            {
                incomingDataToolStripMenuItem.DropDownItems.Add(formatName);
                outgoingDataToolStripMenuItem.DropDownItems.Add(formatName);
                baseToolStripMenuItem.DropDownItems.Add(formatName);
            }
            incomingDataToolStripMenuItem.Tag = "In";
            outgoingDataToolStripMenuItem.Tag = "Out";
            baseToolStripMenuItem.Tag = "Send";

            
            IncomingDataFormat = PrintDataFormat.Decimal;
            OutgoingDataFormat = PrintDataFormat.Decimal;
            SendDataInputFormat = PrintDataFormat.Decimal;

        }

        private PrintDataFormat prvtIncomingDataFormat = PrintDataFormat.Decimal;
        private PrintDataFormat IncomingDataFormat
        {
            get
            {
                return prvtIncomingDataFormat;
            }
            set 
            {
                for (int i = 0; i < incomingDataToolStripMenuItem.DropDownItems.Count; i++ )
                {
                    bool CheckBox = false;
                    if(incomingDataToolStripMenuItem.DropDownItems[i].Text == Enum.GetName(typeof(PrintDataFormat),value)) {CheckBox = true;}
                    ((ToolStripMenuItem)incomingDataToolStripMenuItem.DropDownItems[i]).Checked = CheckBox;                    
                }

                prvtIncomingDataFormat = value; 
            }
        }

        private PrintDataFormat prvtOutgoingDataFormat = PrintDataFormat.Decimal;
        private PrintDataFormat OutgoingDataFormat
        {
            get
            {
                return prvtOutgoingDataFormat;
            }
            set
            {
                for (int i = 0; i < outgoingDataToolStripMenuItem.DropDownItems.Count; i++)
                {
                    bool CheckBox = false;
                    if (outgoingDataToolStripMenuItem.DropDownItems[i].Text == Enum.GetName(typeof(PrintDataFormat), value)) { CheckBox = true; }
                    ((ToolStripMenuItem)outgoingDataToolStripMenuItem.DropDownItems[i]).Checked = CheckBox;
                }
                prvtOutgoingDataFormat = value;
            }
        }

        private PrintDataFormat prvtSendDataInputFormat = PrintDataFormat.Decimal;
        private PrintDataFormat SendDataInputFormat
        {
            get { return prvtSendDataInputFormat; }
            set
            {
                for (int i = 0; i < baseToolStripMenuItem.DropDownItems.Count; i++)
                {
                    bool CheckBox = false;
                    if (baseToolStripMenuItem.DropDownItems[i].Text == Enum.GetName(typeof(PrintDataFormat), value)) { CheckBox = true; }
                    ((ToolStripMenuItem)baseToolStripMenuItem.DropDownItems[i]).Checked = CheckBox;
                }
                prvtSendDataInputFormat = value;
            }
        }

        private void SetDataFormat(object sender, EventArgs e)
        {
            ToolStripMenuItem Sender = (ToolStripMenuItem)sender;
            ToolStripItemClickedEventArgs E = (ToolStripItemClickedEventArgs)e;
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)E.ClickedItem;
            foreach (PrintDataFormat Format in Enum.GetValues(typeof(PrintDataFormat)))
            {
                if (Format.ToString() == clickedItem.Text)
                {
                    switch ((string)Sender.Tag)
                    {
                        case "In":
                            IncomingDataFormat = Format;
                            break;
                        case "Out":
                            OutgoingDataFormat = Format;
                            break;
                        case "Send":
                            SendDataInputFormat = Format;
                            break;
                        default:
                            break;
                    }         
                }                
            }
        }

        private void UpdatePortNameComboBoxItems()
        {
            portNameComboBox.Items.Clear();
            foreach (string name in System.IO.Ports.SerialPort.GetPortNames())
            {
                portNameComboBox.Items.Add(name);
            }
            if (portNameComboBox.Items.Count > 0) { portNameComboBox.SelectedIndex = 0; }
            if (portNameComboBox.Items.Count == 0) { portNameComboBox.Text = ""; }
        }

        private void DigitalPinRcvd(int PinNumber, FirmataHighLow value)
        {
            if (PinNumber == 13) { MessageBox.Show(PinNumber.ToString() + " " + value.ToString()); }            
        }

        private delegate void SerialDataReceivedPost(byte Data);

        private DateTime LastPostTime;

        private void SerialDataReceived(byte Data)
        {
            if (incomingDataTextBox.InvokeRequired)
            {
                this.BeginInvoke(new SerialDataReceivedPost(SerialDataReceived), new object[] { Data });
            }
            else
            {
                if (LastPostTime == null) { LastPostTime = DateTime.Now; }
                int MillisecondsSinceLastPost = (int)(DateTime.Now - LastPostTime).TotalMilliseconds;
                LastPostTime = DateTime.Now;
                string Delimiter = " ";
                if (MillisecondsSinceLastPost > 1) { Delimiter = Environment.NewLine +"<<"; }

                string printString = "";
                switch (IncomingDataFormat)
                {
                    case PrintDataFormat.Decimal:
                        printString = Convert.ToString(Data);
                        if (printString.Length < 2) { printString = "0" + printString; }
                        if (printString.Length < 3) { printString = "0" + printString; }
                        break;
                    case PrintDataFormat.Hexidecimal:
                        printString = Convert.ToString(Data, 16);
                        if (printString.Length < 2) { printString = "0" + printString; }
                        break;
                    case PrintDataFormat.ASCII:
                        printString = Encoding.ASCII.GetString(new byte[] { Data });
                        break;
                    default:
                        break;
                }
                incomingDataTextBox.AppendText(Delimiter + printString);
            }
        }
        private delegate void SerialDataSentPost(byte[] Data);
        private void SerialDataSent(byte[] Data)
        {
            if (incomingDataTextBox.InvokeRequired)
            {
                this.BeginInvoke(new SerialDataSentPost(SerialDataSent), new object[] { Data });
            }
            else
            {
                incomingDataTextBox.AppendText(Environment.NewLine + ">> ");
                
                for(int i = 0; i < Data.Count(); i++)
                {

                    string DataString = "";
                    switch (OutgoingDataFormat)
                    {
                        case PrintDataFormat.Decimal:
                            DataString = Convert.ToString(Data[i]);
                            if (DataString.Length < 2) { DataString = "0" + DataString; }
                            if (DataString.Length < 3) { DataString = "0" + DataString; }
                            break;
                        case PrintDataFormat.Hexidecimal:
                            DataString = Convert.ToString(Data[i], 16);
                            if (DataString.Length < 2) { DataString = "0" + DataString; }
                            break;
                        default:
                            break;
                    }

                    incomingDataTextBox.AppendText(DataString + " ");
                }

            }
        }

        private void portNameComboBox_DropDown(object sender, EventArgs e)
        {
            UpdatePortNameComboBoxItems(); 
        }
        private void sendTextBox_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = sendButton;
        }
        private void sendTextBox_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }
        private void sendButton_Click(object sender, EventArgs e)
        {
            SendBytes();
        }
        public void SendBytes(byte[] Bytes = null)
        {
            ThreadPool.QueueUserWorkItem(o => prvtSendBytes(Bytes));
        }


        private char prvtSendDelimiter = ' ';
        private void prvtSendBytes(byte[] Bytes = null)
        {
            try
            {

                if (Bytes == null)
                {
                    if (SendDataInputFormat == PrintDataFormat.ASCII)
                    {
                        prvtFirmataPort.SendByteArray(Encoding.ASCII.GetBytes(sendTextBox.Text));
                    }
                    else
                    {
                        string[] ByteStrings = sendTextBox.Text.Split(prvtSendDelimiter);
                        int ByteCount = ByteStrings.Count();
                        Bytes = new byte[ByteCount];
                        for (int i = 0; i < ByteCount; i++)
                        {
                            switch (SendDataInputFormat)
                            {
                                case PrintDataFormat.Decimal:
                                    Bytes[i] = Convert.ToByte(ByteStrings[i]);
                                    break;
                                case PrintDataFormat.Hexidecimal:
                                    Bytes[i] = Convert.ToByte(ByteStrings[i], 16);
                                    break;
                                default:
                                    break;
                            }

                        }
                        prvtFirmataPort.SendByteArray(Bytes);
                    }
                    
                }
                
            }
            catch
            {

            }
        }


        private void setDelimiterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Result = "";
            
            switch (ShowInputDialog(ref Result))
            {
                case DialogResult.OK:
                    prvtSendDelimiter = Result.ToCharArray().First();
                    break;
                case DialogResult.Cancel:
                    MessageBox.Show(Result);
                    break;
                default:
                    break;
            }
        }


        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Name";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }


    }
}
