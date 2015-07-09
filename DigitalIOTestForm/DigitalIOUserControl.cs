using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalIOTestForm
{
    public partial class DigitalIOUserControl : UserControl
    {
        public DigitalIOUserControl(int DigitalPinNumber)
        {
            InitializeComponent();
            if (!StaleTimerInitialized)
            {
                StaleTimer.Interval = 500;
                StaleTimer.Tick += StaleTimerTick;
                StaleTimer.Enabled = true;
                StaleTimerInitialized = true;
            }
            DigitalIOUserControl.ListOfControls.Add(this);
            this.pinModeCheckBox.BackColor = Color.Transparent;
            this.digitalWriteCheckBox.BackColor = Color.Transparent;
            this.digitalPinNumber = DigitalPinNumber;
        }

        public DigitalIOUserControl()
        {
            InitializeComponent();
            if(!StaleTimerInitialized)
            {
                StaleTimer.Interval = 500;
                StaleTimer.Tick += StaleTimerTick;
                StaleTimer.Enabled = true;
                StaleTimerInitialized = true;
            }

            ListOfControls.Add(this);
            this.pinModeCheckBox.BackColor = Color.Transparent;
            this.digitalWriteCheckBox.BackColor = Color.Transparent;
        }
        public bool digitalPinMode
        {
            get
            {
                return this.pinModeCheckBox.BackColor == OutColor;
            }
        }

        public readonly static bool IN = false;
        public readonly static bool OUT = true;

        internal static Color OutColor = Color.DarkGray;
        internal static Color InColor = Color.LightGray;

        private void pinModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.pinModeCheckBox.Checked)
            {
                this.pinModeCheckBox.Text = "OUT";
                this.pinModeCheckBox.BackColor = OutColor;
            }
            else
            {
                this.pinModeCheckBox.Text = "IN";
                this.pinModeCheckBox.BackColor = InColor;
            }
        }

        public int digitalPinNumber
        {
            get
            {
                return Convert.ToInt32(pinNumberLabel.Text);
            }
            set
            {
                if (value < 0 || value > 127) throw new Exception("The firmata protocol only supports digital pin numbrs 0 to 127");
                pinNumberLabel.Text = "000000".Substring(0, (int)Math.Max(3 - (value.ToString()).Length, 0)) + (value.ToString());
            }
        }

        internal static Color HighColor = Color.Red;
        internal static Color LowColor = Color.Salmon;

        public readonly static bool HIGH = true;
        public readonly static bool LOW = false;

        public bool digitalWrite
        {
            get
            {
                return this.digitalWriteCheckBox.BackColor == HighColor;
            }
            set
            {
                this.digitalWriteCheckBox.Checked = value;
            }
        }

        private void digitalWriteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.digitalWriteCheckBox.Checked)
            {
                this.digitalWriteCheckBox.Text = "HIGH";
                this.digitalWriteCheckBox.BackColor = HighColor;
            }
            else
            {
                this.digitalWriteCheckBox.Text = "LOW";
                this.digitalWriteCheckBox.BackColor = LowColor;
            }
        }

        internal static Color OnBackColor = Color.Red;
        internal static Color OffBackColor = Color.LightPink;
        internal static Color UnknownBackColor = Color.White;

        private void updateDigitalRead(bool State)
        {
            LastUpdateTimeStamp = DateTime.Now;
            bool digitalReadStateChanged = false;
            if (State != digitalReadState) digitalReadStateChanged = true;

            if(State)
            {
                this.digitalReadRadioButton.BackColor = DigitalIOUserControl.OnBackColor;
            }
            else
            {
                this.digitalReadRadioButton.BackColor = DigitalIOUserControl.OffBackColor;
            }
            if (digitalReadUpdated != null) digitalReadUpdated(this, EventArgs.Empty);
            if (digitalReadStateChanged && digitalReadChanged != null) digitalReadChanged(this, EventArgs.Empty);
        }

        public bool digitalReadState
        {
            get
            {
                return this.digitalReadRadioButton.BackColor == DigitalIOUserControl.OnBackColor;
            }
            set
            {
                updateDigitalRead(value);
            }
        }
                
        public event EventHandler digitalReadUpdated;
        public event EventHandler digitalReadChanged;
        public event EventHandler digitalReadIsStale;
        public event EventHandler pinNumberLabel_ClickEventHandler;

        private void pinNumberLabel_Click(object sender, EventArgs e)
        {
            if (pinNumberLabel_ClickEventHandler != null) pinNumberLabel_ClickEventHandler(sender, e);
        }
        
        public static bool StaleCheckTimerEnable
        {
            get
            {
                return StaleTimer.Enabled;
            }
            set
            {
                StaleTimer.Enabled = value;
            }
        }

        public static int StaleCheckPeriod
        {
            get
            {
                return StaleTimer.Interval;
            }
            set
            {
                StaleTimer.Interval = value;
            }
        }

        internal static bool StaleTimerInitialized = false;

        internal static Timer StaleTimer = new Timer();
        internal static List<DigitalIOUserControl> ListOfControls = new List<DigitalIOUserControl>();
        internal static void StaleTimerTick(object sender, EventArgs e)
        {
            for (int I = 0; I < 128; I++ )
            {
                 ListOfControls[I].PeriodicExpirationCheck();

            }
        }
        private long prvtLifeSpan = -1;
        /// <summary>
        /// The number of milliseconds that may pass between a read update and the stale event.  Set to -1 to disable the stale event.
        /// </summary>
        public long LifeSpan
        {
            get { return prvtLifeSpan; }
            set 
            {
                if (value < 0)
                {
                    staleCheckBox.Visible = false;
                    this.MinimumSize = new Size(170, 25);
                    this.MaximumSize = new Size(170, 25);
                    this.Size = new Size(170, 25);
                }                    
                else
                {
                    staleCheckBox.Visible = true;
                    this.MaximumSize = new Size(200, 25);
                    this.MinimumSize = new Size(200,25);
                    this.Size = new Size(200, 25);
                }

                if (value < -1) value = -1;
                prvtLifeSpan = value;
            }
        }

        public bool IsExpired
        {
            get
            {
                if (LifeSpan == -1) return false;
                if (MillisSinceReadUpdate > LifeSpan) return true;
                return false;
            }
        }
        public double MillisSinceReadUpdate
        {
            get
            {
                return (DateTime.Now - LastUpdateTimeStamp).TotalMilliseconds;
            }
        }

        internal void PeriodicExpirationCheck(object sender = null, EventArgs e = null)
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(PeriodicExpirationCheck), new object[] { sender, e });
            }
            else
            {
                if (IsExpired)
                {
                    this.staleCheckBox.Checked = true;
                    if (digitalReadIsStale != null) digitalReadIsStale(this, EventArgs.Empty);
                }
                else
                {
                    if (this.staleCheckBox.Checked) this.staleCheckBox.Checked = false;
                }

            }
            
        }
        
        private DateTime LastUpdateTimeStamp = DateTime.Now;

        internal static Color StaleColor = Color.IndianRed;
        internal static Color FreshColor = Color.LightGreen;

        private void staleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(staleCheckBox.Checked)
            {
                staleCheckBox.BackColor = StaleColor;
            }
            else
            {
                staleCheckBox.BackColor = FreshColor;
            }
        }


    }
}
