namespace OneClickSaveTest
{
    partial class OneClickSaveForm
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
            this.components = new System.ComponentModel.Container();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.startButton = new System.Windows.Forms.Button();
            this.setDirectoryButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveScreenshotCheckbox = new System.Windows.Forms.CheckBox();
            this.saveCSVcheckbox = new System.Windows.Forms.CheckBox();
            this.saveSNPcheckbox = new System.Windows.Forms.CheckBox();
            this.saveTemplateCSVcheckbox = new System.Windows.Forms.CheckBox();
            this.directoryText = new System.Windows.Forms.TextBox();
            this.prefixTextBox = new System.Windows.Forms.TextBox();
            this.suffixTextBox = new System.Windows.Forms.TextBox();
            this.prefixLabel = new System.Windows.Forms.Label();
            this.suffixLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.exampleLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lanProtocolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hiSLIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SocketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.f12ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableTimeoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTimeoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeoutTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numberingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startNumberingAtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startNumberingTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.numberOfDigitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numberOfDigitsTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.channelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSNPDataFromChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelListTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.sNPoverrideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SNPautoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snpOverrideSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.port1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.port2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.port3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.port4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutOneClickSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 225);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(316, 65);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "GO";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // setDirectoryButton
            // 
            this.setDirectoryButton.Location = new System.Drawing.Point(12, 27);
            this.setDirectoryButton.Name = "setDirectoryButton";
            this.setDirectoryButton.Size = new System.Drawing.Size(77, 23);
            this.setDirectoryButton.TabIndex = 1;
            this.setDirectoryButton.Text = "Set Directory";
            this.setDirectoryButton.UseVisualStyleBackColor = true;
            this.setDirectoryButton.Click += new System.EventHandler(this.setDirectoryButton_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // saveScreenshotCheckbox
            // 
            this.saveScreenshotCheckbox.AutoSize = true;
            this.saveScreenshotCheckbox.Checked = true;
            this.saveScreenshotCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveScreenshotCheckbox.Location = new System.Drawing.Point(99, 131);
            this.saveScreenshotCheckbox.Name = "saveScreenshotCheckbox";
            this.saveScreenshotCheckbox.Size = new System.Drawing.Size(138, 17);
            this.saveScreenshotCheckbox.TabIndex = 3;
            this.saveScreenshotCheckbox.Text = "Save Screenshot (.png)";
            this.saveScreenshotCheckbox.UseVisualStyleBackColor = true;
            this.saveScreenshotCheckbox.CheckedChanged += new System.EventHandler(this.saveScreenshotCheckbox_CheckedChanged);
            // 
            // saveCSVcheckbox
            // 
            this.saveCSVcheckbox.AutoSize = true;
            this.saveCSVcheckbox.Checked = true;
            this.saveCSVcheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveCSVcheckbox.Location = new System.Drawing.Point(99, 154);
            this.saveCSVcheckbox.Name = "saveCSVcheckbox";
            this.saveCSVcheckbox.Size = new System.Drawing.Size(130, 17);
            this.saveCSVcheckbox.TabIndex = 4;
            this.saveCSVcheckbox.Text = "Save CSV Data (.csv)";
            this.saveCSVcheckbox.UseVisualStyleBackColor = true;
            this.saveCSVcheckbox.CheckedChanged += new System.EventHandler(this.saveCSVcheckbox_CheckedChanged);
            // 
            // saveSNPcheckbox
            // 
            this.saveSNPcheckbox.AutoSize = true;
            this.saveSNPcheckbox.Checked = true;
            this.saveSNPcheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveSNPcheckbox.Location = new System.Drawing.Point(99, 177);
            this.saveSNPcheckbox.Name = "saveSNPcheckbox";
            this.saveSNPcheckbox.Size = new System.Drawing.Size(129, 17);
            this.saveSNPcheckbox.TabIndex = 5;
            this.saveSNPcheckbox.Text = "Save SNP Data (.s*p)";
            this.saveSNPcheckbox.UseVisualStyleBackColor = true;
            this.saveSNPcheckbox.CheckedChanged += new System.EventHandler(this.saveSNPcheckbox_CheckedChanged);
            // 
            // saveTemplateCSVcheckbox
            // 
            this.saveTemplateCSVcheckbox.AutoSize = true;
            this.saveTemplateCSVcheckbox.Checked = true;
            this.saveTemplateCSVcheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveTemplateCSVcheckbox.Location = new System.Drawing.Point(99, 200);
            this.saveTemplateCSVcheckbox.Name = "saveTemplateCSVcheckbox";
            this.saveTemplateCSVcheckbox.Size = new System.Drawing.Size(130, 17);
            this.saveTemplateCSVcheckbox.TabIndex = 6;
            this.saveTemplateCSVcheckbox.Text = "Save Template CSV Data (.csv)";
            this.saveTemplateCSVcheckbox.UseVisualStyleBackColor = true;
            this.saveTemplateCSVcheckbox.CheckedChanged += new System.EventHandler(this.saveTemplateCSVcheckbox_CheckedChanged);
            // 
            // directoryText
            // 
            this.directoryText.Location = new System.Drawing.Point(99, 29);
            this.directoryText.Multiline = true;
            this.directoryText.Name = "directoryText";
            this.directoryText.ReadOnly = true;
            this.directoryText.Size = new System.Drawing.Size(229, 23);
            this.directoryText.TabIndex = 6;
            // 
            // prefixTextBox
            // 
            this.prefixTextBox.Location = new System.Drawing.Point(12, 81);
            this.prefixTextBox.Name = "prefixTextBox";
            this.prefixTextBox.Size = new System.Drawing.Size(148, 20);
            this.prefixTextBox.TabIndex = 7;
            this.prefixTextBox.TextChanged += new System.EventHandler(this.prefixTextBox_TextChanged);
            // 
            // suffixTextBox
            // 
            this.suffixTextBox.Location = new System.Drawing.Point(176, 81);
            this.suffixTextBox.Name = "suffixTextBox";
            this.suffixTextBox.Size = new System.Drawing.Size(152, 20);
            this.suffixTextBox.TabIndex = 8;
            this.suffixTextBox.TextChanged += new System.EventHandler(this.suffixTextBox_TextChanged);
            // 
            // prefixLabel
            // 
            this.prefixLabel.AutoSize = true;
            this.prefixLabel.Location = new System.Drawing.Point(69, 66);
            this.prefixLabel.Name = "prefixLabel";
            this.prefixLabel.Size = new System.Drawing.Size(33, 13);
            this.prefixLabel.TabIndex = 10;
            this.prefixLabel.Text = "Prefix";
            // 
            // suffixLabel
            // 
            this.suffixLabel.AutoSize = true;
            this.suffixLabel.Location = new System.Drawing.Point(241, 66);
            this.suffixLabel.Name = "suffixLabel";
            this.suffixLabel.Size = new System.Drawing.Size(33, 13);
            this.suffixLabel.TabIndex = 11;
            this.suffixLabel.Text = "Suffix";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.exampleLabel, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 107);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(316, 18);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // exampleLabel
            // 
            this.exampleLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.exampleLabel.AutoSize = true;
            this.exampleLabel.Location = new System.Drawing.Point(128, 2);
            this.exampleLabel.Name = "exampleLabel";
            this.exampleLabel.Size = new System.Drawing.Size(60, 13);
            this.exampleLabel.TabIndex = 14;
            this.exampleLabel.Text = "1_CH1.s2p";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(340, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lanProtocolToolStripMenuItem,
            this.triggerToolStripMenuItem,
            this.timeoutToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.systemToolStripMenuItem.Text = "Config";
            // 
            // lanProtocolToolStripMenuItem
            // 
            this.lanProtocolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hiSLIPToolStripMenuItem,
            this.SocketToolStripMenuItem});
            this.lanProtocolToolStripMenuItem.Name = "lanProtocolToolStripMenuItem";
            this.lanProtocolToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.lanProtocolToolStripMenuItem.Text = "LAN Protocol";
            // 
            // hiSLIPToolStripMenuItem
            // 
            this.hiSLIPToolStripMenuItem.Name = "hiSLIPToolStripMenuItem";
            this.hiSLIPToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.hiSLIPToolStripMenuItem.Text = "HiSLIP";
            this.hiSLIPToolStripMenuItem.Click += new System.EventHandler(this.hiSLIPToolStripMenuItem_Click);
            // 
            // SocketToolStripMenuItem
            // 
            this.SocketToolStripMenuItem.Name = "SocketToolStripMenuItem";
            this.SocketToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.SocketToolStripMenuItem.Text = "Sockets LAN";
            this.SocketToolStripMenuItem.Click += new System.EventHandler(this.SocketToolStripMenuItem_Click);
            // 
            // triggerToolStripMenuItem
            // 
            this.triggerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spaceToolStripMenuItem,
            this.f12ToolStripMenuItem,
            this.mButtonToolStripMenuItem});
            this.triggerToolStripMenuItem.Name = "triggerToolStripMenuItem";
            this.triggerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.triggerToolStripMenuItem.Text = "Trigger";
            // 
            // spaceToolStripMenuItem
            // 
            this.spaceToolStripMenuItem.Name = "spaceToolStripMenuItem";
            this.spaceToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.spaceToolStripMenuItem.Text = "Space";
            this.spaceToolStripMenuItem.Click += new System.EventHandler(this.spaceToolStripMenuItem_Click);
            // 
            // f12ToolStripMenuItem
            // 
            this.f12ToolStripMenuItem.Name = "f12ToolStripMenuItem";
            this.f12ToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.f12ToolStripMenuItem.Text = "F12";
            this.f12ToolStripMenuItem.Click += new System.EventHandler(this.f12ToolStripMenuItem_Click);
            // 
            // mButtonToolStripMenuItem
            // 
            this.mButtonToolStripMenuItem.Name = "mButtonToolStripMenuItem";
            this.mButtonToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.mButtonToolStripMenuItem.Text = "Middle Mouse Button";
            this.mButtonToolStripMenuItem.Click += new System.EventHandler(this.mButtonToolStripMenuItem_Click);
            // 
            // timeoutToolStripMenuItem
            // 
            this.timeoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableTimeoutToolStripMenuItem,
            this.setTimeoutToolStripMenuItem});
            this.timeoutToolStripMenuItem.Name = "timeoutToolStripMenuItem";
            this.timeoutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.timeoutToolStripMenuItem.Text = "Timeout";
            // 
            // enableTimeoutToolStripMenuItem
            // 
            this.enableTimeoutToolStripMenuItem.Checked = true;
            this.enableTimeoutToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableTimeoutToolStripMenuItem.Name = "enableTimeoutToolStripMenuItem";
            this.enableTimeoutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.enableTimeoutToolStripMenuItem.Text = "Enable Timeout";
            this.enableTimeoutToolStripMenuItem.Click += new System.EventHandler(this.enableTimeoutToolStripMenuItem_Click);
            // 
            // setTimeoutToolStripMenuItem
            // 
            this.setTimeoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timeoutTextBox});
            this.setTimeoutToolStripMenuItem.Name = "setTimeoutToolStripMenuItem";
            this.setTimeoutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.setTimeoutToolStripMenuItem.Text = "Set Timeout (min)";
            // 
            // timeoutTextBox
            // 
            this.timeoutTextBox.Name = "timeoutTextBox";
            this.timeoutTextBox.Size = new System.Drawing.Size(100, 23);
            this.timeoutTextBox.Text = "60";
            this.timeoutTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.timeoutTextBox_KeyDown);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.numberingToolStripMenuItem,
            this.channelsToolStripMenuItem,
            this.sNPoverrideToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // numberingToolStripMenuItem
            // 
            this.numberingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startNumberingAtToolStripMenuItem,
            this.numberOfDigitsToolStripMenuItem});
            this.numberingToolStripMenuItem.Name = "numberingToolStripMenuItem";
            this.numberingToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.numberingToolStripMenuItem.Text = "Numbering";
            // 
            // startNumberingAtToolStripMenuItem
            // 
            this.startNumberingAtToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startNumberingTextBox});
            this.startNumberingAtToolStripMenuItem.Name = "startNumberingAtToolStripMenuItem";
            this.startNumberingAtToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.startNumberingAtToolStripMenuItem.Text = "Start numbering at";
            // 
            // startNumberingTextBox
            // 
            this.startNumberingTextBox.Name = "startNumberingTextBox";
            this.startNumberingTextBox.Size = new System.Drawing.Size(100, 23);
            this.startNumberingTextBox.Text = "1";
            // 
            // numberOfDigitsToolStripMenuItem
            // 
            this.numberOfDigitsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.numberOfDigitsTextBox});
            this.numberOfDigitsToolStripMenuItem.Name = "numberOfDigitsToolStripMenuItem";
            this.numberOfDigitsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.numberOfDigitsToolStripMenuItem.Text = "Number of digits";
            // 
            // numberOfDigitsTextBox
            // 
            this.numberOfDigitsTextBox.Name = "numberOfDigitsTextBox";
            this.numberOfDigitsTextBox.Size = new System.Drawing.Size(100, 23);
            this.numberOfDigitsTextBox.Text = "1";
            // 
            // channelsToolStripMenuItem
            // 
            this.channelsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveSNPDataFromChannelsToolStripMenuItem});
            this.channelsToolStripMenuItem.Name = "channelsToolStripMenuItem";
            this.channelsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.channelsToolStripMenuItem.Text = "Channels";
            // 
            // saveSNPDataFromChannelsToolStripMenuItem
            // 
            this.saveSNPDataFromChannelsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channelListTextBox});
            this.saveSNPDataFromChannelsToolStripMenuItem.Name = "saveSNPDataFromChannelsToolStripMenuItem";
            this.saveSNPDataFromChannelsToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.saveSNPDataFromChannelsToolStripMenuItem.Text = "Save SNP data from channels";
            // 
            // channelListTextBox
            // 
            this.channelListTextBox.Name = "channelListTextBox";
            this.channelListTextBox.Size = new System.Drawing.Size(100, 23);
            this.channelListTextBox.Text = "All";
            // 
            // sNPoverrideToolStripMenuItem
            // 
            this.sNPoverrideToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SNPautoToolStripMenuItem,
            this.snpOverrideSeparator1,
            this.port1ToolStripMenuItem,
            this.port2ToolStripMenuItem,
            this.port3ToolStripMenuItem,
            this.port4ToolStripMenuItem});
            this.sNPoverrideToolStripMenuItem.Name = "sNPoverrideToolStripMenuItem";
            this.sNPoverrideToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.sNPoverrideToolStripMenuItem.Text = "Save SNP data from ports";
            // 
            // SNPautoToolStripMenuItem
            // 
            this.SNPautoToolStripMenuItem.Checked = true;
            this.SNPautoToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SNPautoToolStripMenuItem.Name = "SNPautoToolStripMenuItem";
            this.SNPautoToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.SNPautoToolStripMenuItem.Text = "Auto";
            this.SNPautoToolStripMenuItem.Click += new System.EventHandler(this.SNPautoToolStripMenuItem_Click);
            // 
            // snpOverrideSeparator1
            // 
            this.snpOverrideSeparator1.Name = "snpOverrideSeparator1";
            this.snpOverrideSeparator1.Size = new System.Drawing.Size(97, 6);
            this.snpOverrideSeparator1.Visible = false;
            // 
            // port1ToolStripMenuItem
            // 
            this.port1ToolStripMenuItem.Checked = true;
            this.port1ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.port1ToolStripMenuItem.Name = "port1ToolStripMenuItem";
            this.port1ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.port1ToolStripMenuItem.Text = "1";
            this.port1ToolStripMenuItem.Visible = false;
            this.port1ToolStripMenuItem.Click += new System.EventHandler(this.port1ToolStripMenuItem_Click);
            // 
            // port2ToolStripMenuItem
            // 
            this.port2ToolStripMenuItem.Checked = true;
            this.port2ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.port2ToolStripMenuItem.Name = "port2ToolStripMenuItem";
            this.port2ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.port2ToolStripMenuItem.Text = "2";
            this.port2ToolStripMenuItem.Visible = false;
            this.port2ToolStripMenuItem.Click += new System.EventHandler(this.port2ToolStripMenuItem_Click);
            // 
            // port3ToolStripMenuItem
            // 
            this.port3ToolStripMenuItem.Checked = true;
            this.port3ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.port3ToolStripMenuItem.Name = "port3ToolStripMenuItem";
            this.port3ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.port3ToolStripMenuItem.Text = "3";
            this.port3ToolStripMenuItem.Visible = false;
            this.port3ToolStripMenuItem.Click += new System.EventHandler(this.port3ToolStripMenuItem_Click);
            // 
            // port4ToolStripMenuItem
            // 
            this.port4ToolStripMenuItem.Checked = true;
            this.port4ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.port4ToolStripMenuItem.Name = "port4ToolStripMenuItem";
            this.port4ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.port4ToolStripMenuItem.Text = "4";
            this.port4ToolStripMenuItem.Visible = false;
            this.port4ToolStripMenuItem.Click += new System.EventHandler(this.port4ToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem,
            this.aboutOneClickSaveToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // aboutOneClickSaveToolStripMenuItem
            // 
            this.aboutOneClickSaveToolStripMenuItem.Name = "aboutOneClickSaveToolStripMenuItem";
            this.aboutOneClickSaveToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.aboutOneClickSaveToolStripMenuItem.Text = "About One-Click Save";
            this.aboutOneClickSaveToolStripMenuItem.Click += new System.EventHandler(this.aboutOneClickSaveToolStripMenuItem_Click);
            // 
            // OneClickSaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 298);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.prefixLabel);
            this.Controls.Add(this.suffixLabel);
            this.Controls.Add(this.prefixTextBox);
            this.Controls.Add(this.directoryText);
            this.Controls.Add(this.suffixTextBox);
            this.Controls.Add(this.saveSNPcheckbox);
            this.Controls.Add(this.saveCSVcheckbox);
            this.Controls.Add(this.saveScreenshotCheckbox);
            this.Controls.Add(this.saveTemplateCSVcheckbox);
            this.Controls.Add(this.setDirectoryButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "OneClickSaveForm";
            this.Text = "One-Click Save";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button setDirectoryButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox saveScreenshotCheckbox;
        private System.Windows.Forms.CheckBox saveCSVcheckbox;
        private System.Windows.Forms.CheckBox saveSNPcheckbox;
        private System.Windows.Forms.CheckBox saveTemplateCSVcheckbox;
        private System.Windows.Forms.TextBox directoryText;
        private System.Windows.Forms.TextBox prefixTextBox;
        private System.Windows.Forms.TextBox suffixTextBox;
        private System.Windows.Forms.Label prefixLabel;
        private System.Windows.Forms.Label suffixLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label exampleLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lanProtocolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SocketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hiSLIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem f12ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem numberingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startNumberingAtToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox startNumberingTextBox;
        private System.Windows.Forms.ToolStripMenuItem numberOfDigitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox numberOfDigitsTextBox;
        private System.Windows.Forms.ToolStripMenuItem channelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSNPDataFromChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox channelListTextBox;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sNPoverrideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SNPautoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem port1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem port2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem port3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem port4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator snpOverrideSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutOneClickSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timeoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableTimeoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setTimeoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox timeoutTextBox;
    }
}

