namespace TestForm
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectButton = new System.Windows.Forms.Button();
            this.portNameComboBox = new System.Windows.Forms.ComboBox();
            this.incomingDataTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.baudRateTextBox = new System.Windows.Forms.TextBox();
            this.sendTextBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displaySettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.incomingDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outgoingDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendAsComboBox = new System.Windows.Forms.ComboBox();
            this.sendSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDelimiterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.translateMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runTranslatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(365, 31);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(117, 20);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // portNameComboBox
            // 
            this.portNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portNameComboBox.FormattingEnabled = true;
            this.portNameComboBox.Location = new System.Drawing.Point(75, 31);
            this.portNameComboBox.Name = "portNameComboBox";
            this.portNameComboBox.Size = new System.Drawing.Size(95, 21);
            this.portNameComboBox.TabIndex = 1;
            this.portNameComboBox.DropDown += new System.EventHandler(this.portNameComboBox_DropDown);
            // 
            // incomingDataTextBox
            // 
            this.incomingDataTextBox.Location = new System.Drawing.Point(12, 61);
            this.incomingDataTextBox.Multiline = true;
            this.incomingDataTextBox.Name = "incomingDataTextBox";
            this.incomingDataTextBox.Size = new System.Drawing.Size(470, 408);
            this.incomingDataTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Port Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Baud Rate:";
            // 
            // baudRateTextBox
            // 
            this.baudRateTextBox.Location = new System.Drawing.Point(243, 31);
            this.baudRateTextBox.Name = "baudRateTextBox";
            this.baudRateTextBox.Size = new System.Drawing.Size(116, 20);
            this.baudRateTextBox.TabIndex = 6;
            this.baudRateTextBox.Text = "57600";
            // 
            // sendTextBox
            // 
            this.sendTextBox.Location = new System.Drawing.Point(12, 475);
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(326, 20);
            this.sendTextBox.TabIndex = 10;
            this.sendTextBox.Enter += new System.EventHandler(this.sendTextBox_Enter);
            this.sendTextBox.Leave += new System.EventHandler(this.sendTextBox_Leave);
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(344, 474);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(68, 21);
            this.sendButton.TabIndex = 11;
            this.sendButton.Text = "Send As";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.displaySettingsToolStripMenuItem,
            this.sendSettingsToolStripMenuItem,
            this.translateMessagesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(493, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // displaySettingsToolStripMenuItem
            // 
            this.displaySettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.incomingDataToolStripMenuItem,
            this.outgoingDataToolStripMenuItem});
            this.displaySettingsToolStripMenuItem.Name = "displaySettingsToolStripMenuItem";
            this.displaySettingsToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.displaySettingsToolStripMenuItem.Text = "Display Settings";
            // 
            // incomingDataToolStripMenuItem
            // 
            this.incomingDataToolStripMenuItem.Name = "incomingDataToolStripMenuItem";
            this.incomingDataToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.incomingDataToolStripMenuItem.Text = "Incoming Data";
            // 
            // outgoingDataToolStripMenuItem
            // 
            this.outgoingDataToolStripMenuItem.Name = "outgoingDataToolStripMenuItem";
            this.outgoingDataToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.outgoingDataToolStripMenuItem.Text = "Outgoing Data";
            // 
            // sendAsComboBox
            // 
            this.sendAsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sendAsComboBox.FormattingEnabled = true;
            this.sendAsComboBox.Items.AddRange(new object[] {
            "Bytes",
            "String"});
            this.sendAsComboBox.Location = new System.Drawing.Point(413, 474);
            this.sendAsComboBox.Name = "sendAsComboBox";
            this.sendAsComboBox.Size = new System.Drawing.Size(64, 21);
            this.sendAsComboBox.TabIndex = 13;
            // 
            // sendSettingsToolStripMenuItem
            // 
            this.sendSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setDelimiterToolStripMenuItem,
            this.baseToolStripMenuItem});
            this.sendSettingsToolStripMenuItem.Name = "sendSettingsToolStripMenuItem";
            this.sendSettingsToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.sendSettingsToolStripMenuItem.Text = "Send Settings";
            // 
            // setDelimiterToolStripMenuItem
            // 
            this.setDelimiterToolStripMenuItem.Name = "setDelimiterToolStripMenuItem";
            this.setDelimiterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.setDelimiterToolStripMenuItem.Text = "Set Delimiter";
            this.setDelimiterToolStripMenuItem.Click += new System.EventHandler(this.setDelimiterToolStripMenuItem_Click);
            // 
            // baseToolStripMenuItem
            // 
            this.baseToolStripMenuItem.Name = "baseToolStripMenuItem";
            this.baseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.baseToolStripMenuItem.Text = "Base";
            // 
            // translateMessagesToolStripMenuItem
            // 
            this.translateMessagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runTranslatorToolStripMenuItem});
            this.translateMessagesToolStripMenuItem.Name = "translateMessagesToolStripMenuItem";
            this.translateMessagesToolStripMenuItem.Size = new System.Drawing.Size(121, 20);
            this.translateMessagesToolStripMenuItem.Text = "Translate Messages";
            // 
            // runTranslatorToolStripMenuItem
            // 
            this.runTranslatorToolStripMenuItem.Name = "runTranslatorToolStripMenuItem";
            this.runTranslatorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.runTranslatorToolStripMenuItem.Text = "Run Translator";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 498);
            this.Controls.Add(this.sendAsComboBox);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.sendTextBox);
            this.Controls.Add(this.baudRateTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.incomingDataTextBox);
            this.Controls.Add(this.portNameComboBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Serial Connection";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.ComboBox portNameComboBox;
        private System.Windows.Forms.TextBox incomingDataTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox baudRateTextBox;
        private System.Windows.Forms.TextBox sendTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displaySettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem incomingDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outgoingDataToolStripMenuItem;
        private System.Windows.Forms.ComboBox sendAsComboBox;
        private System.Windows.Forms.ToolStripMenuItem sendSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDelimiterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem translateMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runTranslatorToolStripMenuItem;

    }
}

