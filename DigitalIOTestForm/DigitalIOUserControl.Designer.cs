namespace DigitalIOTestForm
{
    partial class DigitalIOUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pinNumberLabel = new System.Windows.Forms.Label();
            this.pinModeCheckBox = new System.Windows.Forms.CheckBox();
            this.digitalWriteCheckBox = new System.Windows.Forms.CheckBox();
            this.digitalReadRadioButton = new System.Windows.Forms.RadioButton();
            this.staleCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // pinNumberLabel
            // 
            this.pinNumberLabel.AutoSize = true;
            this.pinNumberLabel.Location = new System.Drawing.Point(7, 6);
            this.pinNumberLabel.Name = "pinNumberLabel";
            this.pinNumberLabel.Size = new System.Drawing.Size(25, 13);
            this.pinNumberLabel.TabIndex = 0;
            this.pinNumberLabel.Text = "001";
            this.pinNumberLabel.Click += new System.EventHandler(this.pinNumberLabel_Click);
            // 
            // pinModeCheckBox
            // 
            this.pinModeCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.pinModeCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pinModeCheckBox.Location = new System.Drawing.Point(38, 0);
            this.pinModeCheckBox.Name = "pinModeCheckBox";
            this.pinModeCheckBox.Size = new System.Drawing.Size(45, 25);
            this.pinModeCheckBox.TabIndex = 1;
            this.pinModeCheckBox.Text = "IN";
            this.pinModeCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pinModeCheckBox.UseVisualStyleBackColor = true;
            this.pinModeCheckBox.CheckedChanged += new System.EventHandler(this.pinModeCheckBox_CheckedChanged);
            // 
            // digitalWriteCheckBox
            // 
            this.digitalWriteCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.digitalWriteCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.digitalWriteCheckBox.Location = new System.Drawing.Point(89, 0);
            this.digitalWriteCheckBox.Name = "digitalWriteCheckBox";
            this.digitalWriteCheckBox.Size = new System.Drawing.Size(44, 25);
            this.digitalWriteCheckBox.TabIndex = 2;
            this.digitalWriteCheckBox.Text = "LOW";
            this.digitalWriteCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.digitalWriteCheckBox.UseVisualStyleBackColor = true;
            this.digitalWriteCheckBox.CheckedChanged += new System.EventHandler(this.digitalWriteCheckBox_CheckedChanged);
            // 
            // digitalReadRadioButton
            // 
            this.digitalReadRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.digitalReadRadioButton.AutoCheck = false;
            this.digitalReadRadioButton.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.digitalReadRadioButton.Checked = true;
            this.digitalReadRadioButton.Enabled = false;
            this.digitalReadRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.digitalReadRadioButton.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.digitalReadRadioButton.Location = new System.Drawing.Point(139, 0);
            this.digitalReadRadioButton.Name = "digitalReadRadioButton";
            this.digitalReadRadioButton.Size = new System.Drawing.Size(30, 25);
            this.digitalReadRadioButton.TabIndex = 3;
            this.digitalReadRadioButton.TabStop = true;
            this.digitalReadRadioButton.UseVisualStyleBackColor = true;
            // 
            // staleCheckBox
            // 
            this.staleCheckBox.AutoCheck = false;
            this.staleCheckBox.AutoSize = true;
            this.staleCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.staleCheckBox.Location = new System.Drawing.Point(175, 6);
            this.staleCheckBox.Name = "staleCheckBox";
            this.staleCheckBox.Size = new System.Drawing.Size(12, 11);
            this.staleCheckBox.TabIndex = 4;
            this.staleCheckBox.UseVisualStyleBackColor = true;
            this.staleCheckBox.Visible = false;
            this.staleCheckBox.CheckedChanged += new System.EventHandler(this.staleCheckBox_CheckedChanged);
            // 
            // DigitalIOUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.staleCheckBox);
            this.Controls.Add(this.digitalReadRadioButton);
            this.Controls.Add(this.digitalWriteCheckBox);
            this.Controls.Add(this.pinModeCheckBox);
            this.Controls.Add(this.pinNumberLabel);
            this.MaximumSize = new System.Drawing.Size(200, 25);
            this.MinimumSize = new System.Drawing.Size(200, 25);
            this.Name = "DigitalIOUserControl";
            this.Size = new System.Drawing.Size(200, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pinNumberLabel;
        private System.Windows.Forms.CheckBox pinModeCheckBox;
        private System.Windows.Forms.CheckBox digitalWriteCheckBox;
        private System.Windows.Forms.RadioButton digitalReadRadioButton;
        private System.Windows.Forms.CheckBox staleCheckBox;
    }
}
