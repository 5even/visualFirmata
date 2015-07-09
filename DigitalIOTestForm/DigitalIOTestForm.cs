using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalIOTestForm
{
    public partial class DigitalIOTestForm : Form
    {
        public DigitalIOTestForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            int RowCount = 8;
            this.Width = 200 * 17;
            this.Height = 30 * (128/RowCount+1) + 10 + menuStrip1.Height;
            for (int pinNumber = 0; pinNumber < 128; pinNumber++)
            {
                DigitalIOUserControl NewPinControl = new DigitalIOUserControl(pinNumber);
                this.Controls.Add(NewPinControl);
                NewPinControl.Location = new Point((200) * (pinNumber % RowCount), menuStrip1.Height + 30*((int)(pinNumber / RowCount)));
                NewPinControl.LifeSpan = 1000;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
