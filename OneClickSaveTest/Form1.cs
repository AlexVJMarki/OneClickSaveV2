using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Ivi.Visa.Interop;
using System.Reflection;
using System.Diagnostics;


//Shift + F7 changes to design view

namespace OneClickSaveTest
{
    public enum ptc
    {
        SOCKET,
        HiSLIP
    }

    public partial class OneClickSaveForm : Form
    {
        //Variable initialization
        string saveDirectory = "";
        bool testLoop = false;
        bool saveScreenshot = true;
        bool saveCSV = true;
        bool saveSNP = true;
        bool saveTemplateCSV = true;
        string prefix = "";
        string suffix = "";
        int startNum = 1;
        int minNumberLength = Properties.Settings.Default.NumberLength;
        int[] saveSNPchannel = new int[] { -1 }; //-1 saves from all channels
        bool[] snpOverrideList = new bool[4] { true, true, true, true };
        bool snpOverride = false;
        int timeoutCounter = 0;

        //Reads in values from settings file
        ptc protocol = (ptc)Properties.Settings.Default.Protocol;
        key triggerKey = (key)Properties.Settings.Default.Key;
        int timeout = Properties.Settings.Default.Timeout;
        bool timeoutEnable = Properties.Settings.Default.TimeoutEnable;

        //Used for polling if a key is pressed.
        [DllImport("user32.dll", SetLastError = true)]
        public static extern short GetAsyncKeyState(int key);
        //private const int VK_SPACE = 0x20;
        //private const int VK_MBUTTON = 0x04;
        //private const int VK_F12 = 0x7B;
        public enum key
        {
            SPACE = 0x20,
            F12 = 0x7B,
            MBUTTON = 0x04,
            RETURN = 0x0D
        }
        //Virtual key codes listed here: https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes

        /*Checks if the program is already running. Doesn't work on all machines. Replaced in Program.cs
        private static bool IsAlreadyRunning()
        {
            string strLoc = Assembly.GetExecutingAssembly().Location;
            FileSystemInfo fileInfo = new FileInfo(strLoc);
            string sExeName = fileInfo.Name;
            bool bCreatedNew;

            Mutex mutex = new Mutex(true, "Global\\" + sExeName, out bCreatedNew);
            if (bCreatedNew)
                mutex.ReleaseMutex();

            return !bCreatedNew;
        }
        */

        public OneClickSaveForm()
        {
            InitializeComponent();

            //Adds callbacks not created in InitializeComponent(), mostly for data validation
            this.Closing += MainWindow_Closing;
            this.startNumberingTextBox.LostFocus += new System.EventHandler(this.startNumberingTextBox_LostFocus);
            this.numberOfDigitsTextBox.LostFocus += new System.EventHandler(this.numberOfDigitsTextBox_LostFocus);
            this.channelListTextBox.LostFocus += new System.EventHandler(this.channelListTextBox_LostFocus);
            this.channelListTextBox.KeyDown += channelListTextBox_KeyDown;
            this.startNumberingTextBox.KeyDown += startNumberingTextBox_KeyDown;
            this.numberOfDigitsTextBox.KeyDown += numberOfDigitsTextBox_KeyDown;
            this.startNumberingTextBox.TextChanged += new System.EventHandler(this.startNumberingTextBox_TextChanged);
            this.numberOfDigitsTextBox.TextChanged += new System.EventHandler(this.numberOfDigitsTextBox_TextChanged);
            this.sNPoverrideToolStripMenuItem.DropDown.Closing += new ToolStripDropDownClosingEventHandler(DropDown_Closing);
            this.timeoutTextBox.LostFocus += new System.EventHandler(this.timeoutTextBox_LostFocus);

            //Reads in settings and sets GUI to reflect them.
            if (protocol == ptc.SOCKET)
                SocketToolStripMenuItem.Checked = true;
            else if (protocol == ptc.HiSLIP)
                hiSLIPToolStripMenuItem.Checked = true;

            if (triggerKey == key.SPACE)
                spaceToolStripMenuItem.Checked = true;
            else if (triggerKey == key.F12)
                f12ToolStripMenuItem.Checked = true;
            else if (triggerKey == key.MBUTTON)
                mButtonToolStripMenuItem.Checked = true;

            if(!timeoutEnable)
            {
                enableTimeoutToolStripMenuItem.Checked = false;
                setTimeoutToolStripMenuItem.Enabled = false;
            }

            timeoutTextBox.Text = timeout.ToString();
            
            numberOfDigitsTextBox.Text = minNumberLength.ToString();
            exampleLabel.Text = prefix + generateDeviceNumber(startNum) + "_CH1" + suffix + ".s2p";

            //Initializes error log
            //Stream errorStream = File.Create("errorlog.txt");
            //TextWriterTraceListener errorLog = new TextWriterTraceListener(errorStream);
            //Trace.Listeners.Add(errorLog);
            //Trace.Write("----Program startup at " + DateTime.Now + ".-----");
            //Trace.Flush();
        }
        
        private void setDirectoryButton_Click(object sender, EventArgs e)
        {
            //Allows user to define directory using the folder browers dialog.
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                saveDirectory = folderBrowserDialog1.SelectedPath;

            //Updates directory text box. Currently read only.
            directoryText.Text = saveDirectory;
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            //When user hits start button, a connection is established with local host. 
            //When hit again, disconnects from socket.
            //Won't do anything if there's no directory selected.
            if (saveDirectory != "")
            {
                if (testLoop == false)
                {
                    startButton.Text = "Connecting...";
                    int connected = PNA.StartClient(saveDirectory, protocol);
                    if (connected == 1)
                    {
                        startButton.Text = "Connection Failed";
                    }
                    else
                    {
                        SocketToolStripMenuItem.Enabled = false;
                        hiSLIPToolStripMenuItem.Enabled = false;
                        testLoop = true;
                        startButton.Text = "Running";
                        timer1.Start();
                    }
                }
                else
                {
                    testLoop = false;
                    timer1.Stop();
                    PNA.Disconnect();
                    SocketToolStripMenuItem.Enabled = true;
                    hiSLIPToolStripMenuItem.Enabled = true;
                    startButton.Text = "GO";
                }
            }
            else
                startButton.Text = "No Directory";
        }

        //Main testing loop
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Checks timeout if appropriate
            if (timeoutEnable)
            {
                timeoutCounter++;
                if ((double)timeoutCounter * timer1.Interval/1000  >= timeout*60)
                {
                    testLoop = false;
                    timeoutCounter = 0;
                    timer1.Stop();
                    PNA.Disconnect();
                    SocketToolStripMenuItem.Enabled = true;
                    hiSLIPToolStripMenuItem.Enabled = true;
                    startButton.Text = "Timed Out";
                    return;
                }
            }

            //Every tick, polls the space key.
            if (((GetAsyncKeyState((int)triggerKey) & 0x8000) != 0) && testLoop)
            {
                //Resets timer to keep track of timeout;
                timeoutCounter = 0;

                //Checks to see if file name already exists in selected directory. If it does, 
                //proposes a new name and checks again
                string[] fileList = Directory.GetFiles(saveDirectory);

                //removes directory from file names
                for (int idx = 0; idx < fileList.Length; idx++)
                {
                    fileList[idx] = fileList[idx].Replace(saveDirectory + "\\", "");
                }
                bool matchFound = false;
                int deviceNum = startNum;
                string saveName;
                do
                {
                    saveName = generateDeviceNumber(deviceNum);
                    //Checks all the files in the directory the fit the naming convention of the program for one that uses the proposed
                    //test number. If one is found that meets the naming convention and number, picks the next number. File names are not 
                    //case sensitive but do consider the prefix and suffix as part of the naming convention ie. changing the prefix changes
                    //the pattern that the program serches for, allowing for multiple series of data to exist in one directory.

                    //Regular expression for checking file names
                    //(?i)^(PREFIX)0*(DEVICENUMBER)(_CH\d+)?(SUFFIX).(png|csv|s\dp) 
                    //where DEVICENUMBER is a number (obviously)
                    Regex rgx = new Regex(@"(?i)^(" + prefix + @")0*" + deviceNum + @"(_CH\d+)?(" + suffix + @").(png|csv|s\dp)");
                    matchFound = false;
                    for (int idx = 0; (idx < fileList.Length) && !matchFound; idx++)
                    {
                        if (rgx.IsMatch(fileList[idx]))
                        {
                            matchFound = true;
                            deviceNum++;
                        }
                    }

                } while (matchFound);

                //Aborts all active sweeps and sets all traces to hold.
                PNA.TryParse("ABORt");

                ///Iterates thgouh each channel and holds traces.

                //Fetches list of active channels. Ignores channels above 100. 
                string chnNbrStr = "";
                PNA.TryParse("SYST:CHAN:CAT?", ref chnNbrStr);
                string[] chnNbr = PNA.parseStringArray(chnNbrStr);
                int[] activeChannels = PNA.CSSAtoIntArray(chnNbr);

                while (activeChannels[activeChannels.Length - 1] >= 101)
                {
                    Array.Resize(ref activeChannels, activeChannels.Length - 1);
                }

                //Qureies every active channel for if it's on continuous or not. If it is, it sets
                //The channel to hold. It will be returned to continuous after saving data.
                List<int> isContinuous = new List<int>();
                foreach (var channel in activeChannels)
                {
                    string measNamesStr = "";
                    if (PNA.TryParse("CALC" + channel + ":PAR:CAT:EXT?", ref measNamesStr) == 1)
                        continue;

                    string[] measNames = PNA.parseStringArray(measNamesStr);
                    PNA.TryParse("CALC" + channel + ":PAR:SEL '" + measNames[0] + "'");

                    string thisContinuous = "";
                    PNA.TryParse("SENS" + channel + ":SWE:MODE?", ref thisContinuous);

                    if (thisContinuous == "CONT")
                    {
                        isContinuous.Add(channel);
                        PNA.TryParse("SENS" + channel + ":SWEP:MODE HOLD");
                    }
                }

                ///Saves data depending on which file type(s) are chosen. Screenshot is saved last beacuse it provides the most obvious visual cue
                ///that the save data sequence has been completed. 
                if (saveSNP)
                {
                    //Sets save type to Log magnitude / degrees
                    PNA.TryParse("MMEM:STOR:TRAC:FORM:SNP DB");
                    
                    //sets list of channels to save from. If all (-1) is selected, saves from all active channels.
                    int[] chnList = new int[0];
                    if (saveSNPchannel[0] == -1)
                    {
                        Array.Resize(ref chnList, activeChannels.Length);
                        Array.Copy(activeChannels, chnList, activeChannels.Length);
                    }
                    else
                    {
                        for (int idxRequested = 0; idxRequested < saveSNPchannel.Length; idxRequested++)
                        {
                            if (activeChannels.Contains(saveSNPchannel[idxRequested]))
                            {
                                Array.Resize(ref chnList, chnList.Length + 1);
                                chnList[chnList.Length - 1] = saveSNPchannel[idxRequested];
                            }
                        }
                    }

                    //Saves an SNP file for each channel with _CH plus the channel number included in the file name.

                    for (int chnIdx = 0; chnIdx < chnList.Length && chnList.Length > 0; chnIdx++)
                    {   
                        PNA.SaveSNP(prefix + saveName + (activeChannels.Length > 1 ? "_CH" + chnList[chnIdx] : "") + suffix, chnList[chnIdx], saveDirectory, snpOverride, snpOverrideList);
                        Thread.Sleep(500);
                    }

                    //Workaround for "Trace not found' bug:
                    //
                    //Deletes offending channels before attempting to save a CSV file.
                    string chnNbrStr2 = "";
                    PNA.TryParse("SYST:CHAN:CAT?", ref chnNbrStr2);
                    string[] chnNbr2 = PNA.parseStringArray(chnNbrStr2);
                    int[] chnList2 = PNA.CSSAtoIntArray(chnNbr2);
                    for (int idx = 0; idx < chnList2.Length; idx++)
                    {
                        if (chnList2[idx] >= 199)
                        {
                            PNA.TryParse("SYST:CHAN:DEL " + chnList2[idx]);
                        }
                    }
                }
                if (saveCSV)
                {
                    PNA.TryParse("MMEMory:STORe:DATA \"" + saveDirectory + "\\" + prefix + saveName + suffix + ".csv\",\"CSV Formatted Data\",\"displayed\",\"displayed\",-1;*OPC?");
                    Thread.Sleep(500);
                }
                if (saveTemplateCSV)
                {
                    SaveTemplateCSV(saveDirectory, prefix + saveName + "_template" + suffix + ".csv");
                    Thread.Sleep(500);
                }
                if (saveScreenshot)
                {
                    PNA.TryParse("HCOPy:FILE \"" + saveDirectory + "\\" + prefix + saveName + suffix + ".png\";*OPC?");
                    Thread.Sleep(200);
                }

                ///Sets the channels back to continuous if needed
                foreach (var channel in isContinuous)
                {
                    PNA.TryParse("SENS" + channel + ":SWEP:MODE CONT");
                }
                
                AutoClosingMessageBox.Show("Saving complete.","One-Click Save", 1000);
            }
        }

        //Select data types to save
        private void saveScreenshotCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (saveScreenshotCheckbox.Checked == false)
                saveScreenshot = false;
            else
                saveScreenshot = true;
        }
        private void saveCSVcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (saveCSVcheckbox.Checked == false)
                saveCSV = false;
            else
                saveCSV = true;
        }
        private void saveSNPcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (saveSNPcheckbox.Checked == false)
                saveSNP = true;
            else
                saveSNP = true;
        }
        private void saveTemplateCSVcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (saveTemplateCSVcheckbox.Checked == false)
                saveTemplateCSV = false;
            else
                saveTemplateCSV = true;
        }
        
        //Logic for checking/unchecking the SNP data override buttons
        private void SNPautoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (snpOverride != false)
            {
                snpOverride = false;
                SNPautoToolStripMenuItem.Checked = true;
                port1ToolStripMenuItem.Visible = false;
                port2ToolStripMenuItem.Visible = false;
                port3ToolStripMenuItem.Visible = false;
                port4ToolStripMenuItem.Visible = false;
                snpOverrideSeparator1.Visible = false;
            }
            else
            {
                snpOverride = true;
                SNPautoToolStripMenuItem.Checked = false;
                port1ToolStripMenuItem.Visible = true;
                port2ToolStripMenuItem.Visible = true;
                port3ToolStripMenuItem.Visible = true;
                port4ToolStripMenuItem.Visible = true;
                snpOverrideSeparator1.Visible = true;
            }
        }
        private void port1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (snpOverrideList[0] != true)
            {
                snpOverrideList[0] = true;
                port1ToolStripMenuItem.Checked = true;
            }
            else
            {
                snpOverrideList[0] = false;
                port1ToolStripMenuItem.Checked = false;
            }
        }
        private void port2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (snpOverrideList[1] != true)
            {
                snpOverrideList[1] = true;
                port2ToolStripMenuItem.Checked = true;
            }
            else
            {
                snpOverrideList[1] = false;
                port2ToolStripMenuItem.Checked = false;
            }
        }
        private void port3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (snpOverrideList[2] != true)
            {
                snpOverrideList[2] = true;
                port3ToolStripMenuItem.Checked = true;
            }
            else
            {
                snpOverrideList[2] = false;
                port3ToolStripMenuItem.Checked = false;
            }
        }
        private void port4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (snpOverrideList[3] != true)
            {
                snpOverrideList[3] = true;
                port4ToolStripMenuItem.Checked = true;
            }
            else
            {
                snpOverrideList[3] = false;
                port4ToolStripMenuItem.Checked = false;
            }
        }

        //Sets prefx and suffix in response to user input
        private void prefixTextBox_TextChanged(object sender, EventArgs e)
        {
            //Checks is the user uses a forbidden character, displays a tool tip, and deletes it from the text box. 
            //Windows cannot save files with a forbidden character.
            var forbiddenChar = new List<string> { "/", "?", "<", ">", "\\", ":", "*", "|", "\"" };
            bool containsForbidChar = false;
            System.Windows.Forms.ToolTip forbiddenCharTooltip = new System.Windows.Forms.ToolTip();
            foreach (string character in forbiddenChar)
            {
                if (prefixTextBox.Text.Contains(character))
                {
                    int index = prefixTextBox.Text.IndexOf(character);
                    prefixTextBox.Text = prefixTextBox.Text.Remove(index, 1);
                    containsForbidChar = true;
                    prefixTextBox.Select(prefixTextBox.Text.Length, 0);
                    prefixTextBox.Focus();
                    prefixTextBox.ScrollToCaret();
                }
            }
            prefix = prefixTextBox.Text;
            exampleLabel.Text = prefix + generateDeviceNumber(startNum) + "_CH1" + suffix + ".s2p";

            //Tool tip logic
            if (containsForbidChar)
            {
                forbiddenCharTooltip.IsBalloon = true;
                forbiddenCharTooltip.ReshowDelay = 1000;
                forbiddenCharTooltip.Show("File name cannot contain the following characters\n/ ? < > \\ : * | or \" ", this.prefixTextBox);
            }
        }
        private void suffixTextBox_TextChanged(object sender, EventArgs e)
        {
            //Checks is the user uses a forbidden character, displays a tool tip, and deletes it from the text box. 
            //Windows cannot save files with a forbidden character.
            var forbiddenChar = new List<string> { "/", "?", "<", ">", "\\", ":", "*", "|", "\"" };
            bool containsForbidChar = false;
            System.Windows.Forms.ToolTip forbiddenCharTooltip = new System.Windows.Forms.ToolTip();
            foreach (string character in forbiddenChar)
            {
                if (suffixTextBox.Text.Contains(character))
                {
                    int index = suffixTextBox.Text.IndexOf(character);
                    suffixTextBox.Text = suffixTextBox.Text.Remove(index, 1);
                    containsForbidChar = true;
                    suffixTextBox.Select(suffixTextBox.Text.Length, 0);
                    suffixTextBox.Focus();
                    suffixTextBox.ScrollToCaret();
                }
            }

            //Checks if the suffix ends in period of space. 
            bool endsInSpaceOrPeriod = false;
            System.Windows.Forms.ToolTip endInSpaceOrPeriodTooltip = new System.Windows.Forms.ToolTip();
            if (suffixTextBox.Text.EndsWith(" ") || suffixTextBox.Text.EndsWith("."))
            {
                suffixTextBox.Text = suffixTextBox.Text.Remove(suffixTextBox.Text.Length-1, 1);
                endsInSpaceOrPeriod = true;
                suffixTextBox.Select(suffixTextBox.Text.Length, 0);
                suffixTextBox.Focus();
                suffixTextBox.ScrollToCaret();
            }

            suffix = suffixTextBox.Text;
            exampleLabel.Text = prefix + generateDeviceNumber(startNum) + "_CH1" + suffix + ".s2p";

            //Tool tip logic
            if (containsForbidChar)
            {
                forbiddenCharTooltip.IsBalloon = true;
                forbiddenCharTooltip.ReshowDelay = 1000;
                forbiddenCharTooltip.Show("Fie name cannot contain the following characters\n/ ? < > \\ : * | or \" ", this.suffixTextBox);
            }
            if (endsInSpaceOrPeriod)
            {
                endInSpaceOrPeriodTooltip.IsBalloon = true;
                endInSpaceOrPeriodTooltip.ReshowDelay = 1000;
                endInSpaceOrPeriodTooltip.Show("File name cannot end with a space or period.", this.suffixTextBox);
            }
        }

        //Logic for checking/unchecking the protocol and trigger button options
        private void SocketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (protocol != ptc.SOCKET)
            {
                protocol = ptc.SOCKET;
                SocketToolStripMenuItem.Checked = true;
                hiSLIPToolStripMenuItem.Checked = false;
            }
        }
        private void hiSLIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (protocol != ptc.HiSLIP)
            {
                protocol = ptc.HiSLIP;
                SocketToolStripMenuItem.Checked = false;
                hiSLIPToolStripMenuItem.Checked = true;
            }
        }
        private void spaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (triggerKey != key.SPACE)
            {
                triggerKey = key.SPACE;
                spaceToolStripMenuItem.Checked = true;
                f12ToolStripMenuItem.Checked = false;
                mButtonToolStripMenuItem.Checked = false;
            }
        }
        private void f12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (triggerKey != key.F12)
            {
                triggerKey = key.F12;
                spaceToolStripMenuItem.Checked = false;
                f12ToolStripMenuItem.Checked = true;
                mButtonToolStripMenuItem.Checked = false;
            }
        }
        private void mButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (triggerKey != key.MBUTTON)
            {
                triggerKey = key.MBUTTON;
                spaceToolStripMenuItem.Checked = false;
                f12ToolStripMenuItem.Checked = false;
                mButtonToolStripMenuItem.Checked = true;
            }
        }

        //Menu strip text boxes: start numbering, number of digits, and channel list
        private void startNumberingTextBox_LostFocus(object sender, EventArgs e)
        {
            int newDeviceName;
            if (Int32.TryParse(startNumberingTextBox.Text, out newDeviceName))
                if (newDeviceName >= 0)
                {
                    startNum = newDeviceName;
                    exampleLabel.Text = prefix + generateDeviceNumber(startNum) + "_CH1" + suffix + ".s2p";
                }
                else
                    startNumberingTextBox.Text = startNum.ToString();
            else
                startNumberingTextBox.Text = startNum.ToString();
        }
        private void startNumberingTextBox_TextChanged(object sender, EventArgs e)
        {
            int newDeviceName;
            if (Int32.TryParse(startNumberingTextBox.Text, out newDeviceName) && newDeviceName > 0)
            {
                startNum = newDeviceName;
                exampleLabel.Text = prefix + generateDeviceNumber(startNum) + "_CH1" + suffix + ".s2p";
            }
        }
        private void startNumberingTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (((GetAsyncKeyState((int)key.RETURN) & 0x8000) != 0))
            {
                exampleLabel.Focus();
            }
        }
        private void numberOfDigitsTextBox_LostFocus(object sender, EventArgs e)
        {
            int newMinNumberLength;
            if (Int32.TryParse(numberOfDigitsTextBox.Text, out newMinNumberLength))
                if (newMinNumberLength > 0)
                {
                    minNumberLength = newMinNumberLength;
                    exampleLabel.Text = prefix + generateDeviceNumber(startNum) + "_CH1" + suffix + ".s2p";
                }
                else
                    numberOfDigitsTextBox.Text = minNumberLength.ToString();
            else
                numberOfDigitsTextBox.Text = minNumberLength.ToString();
        }
        private void numberOfDigitsTextBox_TextChanged(object sender, EventArgs e)
        {
            int newMinNumberLength;
            if (Int32.TryParse(numberOfDigitsTextBox.Text, out newMinNumberLength) && newMinNumberLength > 0)
            {
                minNumberLength = newMinNumberLength;
                exampleLabel.Text = prefix + generateDeviceNumber(startNum) + "_CH1" + suffix + ".s2p";
            }
        }
        private void numberOfDigitsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (((GetAsyncKeyState((int)key.RETURN) & 0x8000) != 0))
            {
                exampleLabel.Focus();
            }
        }
        private void channelListTextBox_LostFocus(object sender, EventArgs e)
        {
            //If the user types "all", sets the program to save from all channels. Otherwise, uses the user's channel list,
            //performing some basic input validation first.
            bool invalidSyntax = false;
            System.Windows.Forms.ToolTip invalidSyntaxTooltip = new System.Windows.Forms.ToolTip();
            if (String.Equals(channelListTextBox.Text, "all", StringComparison.CurrentCultureIgnoreCase))
            {
                saveSNPchannel = new int[] { -1 };
                channelListTextBox.Text = "All";
            }
            else
            {
                Regex rgx = new Regex(@"^\d+(\s*,\s*\d+)*$");
                if (rgx.IsMatch(channelListTextBox.Text))
                {
                    int[] newChannelList = PNA.CSStoIntArray(channelListTextBox.Text);
                    if (Array.Exists(newChannelList, element => element <= 0 || element > 100))
                        invalidSyntax = true;
                    else
                        saveSNPchannel = PNA.CSStoIntArray(channelListTextBox.Text);
                }
                else
                    invalidSyntax = true;
            }

            if (invalidSyntax == true)
            {
                if (saveSNPchannel[0] == -1)
                    channelListTextBox.Text = "All";
                else
                    channelListTextBox.Text = string.Join(",", saveSNPchannel);

                invalidSyntaxTooltip.IsBalloon = true;
                invalidSyntaxTooltip.ReshowDelay = 1000;
                invalidSyntaxTooltip.Show("Channel list must be a list of numbers seperated\n" +
                                          "by commas (ex. '1,2') or the word 'all'.", this.suffixTextBox);
            }
        }
        private void channelListTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (((GetAsyncKeyState((int)key.RETURN) & 0x8000) != 0))
            {
                exampleLabel.Focus();
            }
        }
        private void timeoutTextBox_LostFocus(object sender, EventArgs e)
        {
            int newTimeout;
            if (Int32.TryParse(timeoutTextBox.Text, out newTimeout))
                timeout = newTimeout;
            else
                timeoutTextBox.Text = timeout.ToString();

        }
        private void timeoutTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (((GetAsyncKeyState((int)key.RETURN) & 0x8000) != 0))
            {
                exampleLabel.Focus();
            }
        }

        //Used to assign a new device number to a unit, adding leading zeros as needed
        private string generateDeviceNumber(int number)
        {
            double num = Math.Log10((double)number + 1);
            double digits = Math.Ceiling(num);
            double LeadingZeros = (minNumberLength - digits >= 0) ? (minNumberLength - digits) : 0;
            string result = "";
            for (int idx = 0; idx < LeadingZeros; idx++)
            {
                result += "0";
            }
            result += number.ToString();
            return result;
        }

        //Template_CSV file generation logic - converted from Python script
        //This method generates and saves a Template_CSV file containing channel frequencies and limit line data
        //Filename format: prefix + "template_" + deviceNumber + suffix + ".csv"
        private void SaveTemplateCSV(string directory, string filename)
        {
            try
            {
                string fullPath = directory + "\\" + filename;
                
                // Get all active channels
                List<int> channels = GetActiveChannels();
                if (channels.Count == 0)
                {
                    File.WriteAllText(fullPath, "Error: No active channels found\n");
                    return;
                }

                // Build template data dictionary
                Dictionary<string, object> templateData = ParseChannelsForTemplate(channels);
                
                // Convert to CSV format and save
                WriteTemplateToCsv(templateData, fullPath);
            }
            catch (Exception ex)
            {
                // Log error and create error file
                System.Diagnostics.Trace.WriteLine("SaveTemplateCSV error: " + ex.Message);
                try
                {
                    string errorPath = directory + "\\" + filename;
                    File.WriteAllText(errorPath, "Error generating Template_CSV: " + ex.Message + "\n");
                }
                catch { /* Ignore secondary errors */ }
            }
        }

        private List<int> GetActiveChannels()
        {
            List<int> channels = new List<int>();
            try
            {
                string channelStr = "";
                if (PNA.TryParse("SYST:CHAN:CAT?", ref channelStr) == 0)
                {
                    string[] channelArray = PNA.parseStringArray(channelStr);
                    int[] channelNumbers = PNA.CSSAtoIntArray(channelArray);
                    
                    // Filter out channels above 100 (same logic as existing code)
                    foreach (int ch in channelNumbers)
                    {
                        if (ch <= 100)
                            channels.Add(ch);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("GetActiveChannels error: " + ex.Message);
            }
            return channels;
        }

        private Dictionary<string, object> ParseChannelsForTemplate(List<int> channels)
        {
            Dictionary<string, object> templateData = new Dictionary<string, object>();
            
            foreach (int channel in channels)
            {
                try
                {
                    // Get start and stop frequencies for this channel
                    var frequencies = GetStartStopFrequencies(channel);
                    templateData[$"MinFreq_CH{channel}"] = frequencies.Item1;
                    templateData[$"MaxFreq_CH{channel}"] = frequencies.Item2;

                    // Get traces for this channel
                    Dictionary<string, string> traces = GetTracesInChannel(channel);
                    
                    // For each trace, get limit line information
                    foreach (var trace in traces)
                    {
                        string traceName = trace.Key;
                        
                        // Select the trace
                        PNA.TryParse($"CALC{channel}:PAR:SEL '{traceName}'");
                        
                        // Get limit line data
                        var limitData = GetLimitLineData(channel);
                        templateData[$"Limits_{channel}_{traceName}"] = FormatLimitData(limitData);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine($"Error processing channel {channel}: " + ex.Message);
                    templateData[$"Error_CH{channel}"] = "Failed to process channel";
                }
            }
            
            return templateData;
        }

        private Tuple<double, double> GetStartStopFrequencies(int channel)
        {
            double startFreq = 0.0, stopFreq = 0.0;
            
            try
            {
                string startStr = "";
                if (PNA.TryParse($"SENS{channel}:FREQ:STAR?", ref startStr) == 0)
                {
                    double.TryParse(startStr.Trim().Replace("\n", ""), out startFreq);
                }
                
                string stopStr = "";
                if (PNA.TryParse($"SENS{channel}:FREQ:STOP?", ref stopStr) == 0)
                {
                    double.TryParse(stopStr.Trim().Replace("\n", ""), out stopFreq);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Error getting frequencies for channel {channel}: " + ex.Message);
            }
            
            return new Tuple<double, double>(startFreq, stopFreq);
        }

        private Dictionary<string, string> GetTracesInChannel(int channel)
        {
            Dictionary<string, string> traces = new Dictionary<string, string>();
            
            try
            {
                string tracesStr = "";
                if (PNA.TryParse($"CALC{channel}:PAR:CAT:EXT?", ref tracesStr) == 0)
                {
                    string[] traceArray = PNA.parseStringArray(tracesStr);
                    
                    // Parse pairs: name, type, name, type, etc.
                    for (int i = 0; i < traceArray.Length - 1; i += 2)
                    {
                        string traceName = traceArray[i];
                        string traceType = traceArray[i + 1];
                        traces[traceName] = traceType;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Error getting traces for channel {channel}: " + ex.Message);
            }
            
            return traces;
        }

        private Tuple<List<double>, List<double>, List<double>, List<double>> GetLimitLineData(int channel)
        {
            List<double> limitXStart = new List<double>();
            List<double> limitXStop = new List<double>();
            List<double> limitYStart = new List<double>();
            List<double> limitYStop = new List<double>();
            
            try
            {
                string segmentsStr = "";
                if (PNA.TryParse($"CALC{channel}:LIM:SEGM:COUN?", ref segmentsStr) == 0)
                {
                    int segmentCount = 0;
                    string cleanSegments = segmentsStr.Trim().Replace("\n", "").Replace("+", "");
                    
                    if (int.TryParse(cleanSegments, out segmentCount))
                    {
                        for (int segment = 1; segment <= segmentCount; segment++)
                        {
                            // Get segment starting amplitude
                            string amplStartStr = "";
                            if (PNA.TryParse($"CALC{channel}:LIM:SEGM{segment}:AMPL:STAR?", ref amplStartStr) == 0)
                            {
                                double amplStart = 0.0;
                                double.TryParse(amplStartStr.Trim().Replace("\n", ""), out amplStart);
                                limitYStart.Add(amplStart);
                            }
                            
                            // Get segment stopping amplitude  
                            string amplStopStr = "";
                            if (PNA.TryParse($"CALC{channel}:LIM:SEGM{segment}:AMPL:STOP?", ref amplStopStr) == 0)
                            {
                                double amplStop = 0.0;
                                double.TryParse(amplStopStr.Trim().Replace("\n", ""), out amplStop);
                                limitYStop.Add(amplStop);
                            }
                            
                            // Get segment starting stimulus
                            string stimStartStr = "";
                            if (PNA.TryParse($"CALC{channel}:LIM:SEGM{segment}:STIM:STAR?", ref stimStartStr) == 0)
                            {
                                double stimStart = 0.0;
                                double.TryParse(stimStartStr.Trim().Replace("\n", ""), out stimStart);
                                limitXStart.Add(stimStart);
                            }
                            
                            // Get segment stopping stimulus
                            string stimStopStr = "";
                            if (PNA.TryParse($"CALC{channel}:LIM:SEGM{segment}:STIM:STOP?", ref stimStopStr) == 0)
                            {
                                double stimStop = 0.0;
                                double.TryParse(stimStopStr.Trim().Replace("\n", ""), out stimStop);
                                limitXStop.Add(stimStop);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Error getting limit line data for channel {channel}: " + ex.Message);
            }
            
            return new Tuple<List<double>, List<double>, List<double>, List<double>>(
                limitXStart, limitXStop, limitYStart, limitYStop);
        }

        private string FormatLimitData(Tuple<List<double>, List<double>, List<double>, List<double>> limitData)
        {
            try
            {
                // Format as: "x_start1,x_stop1,y_start1,y_stop1;x_start2,x_stop2,y_start2,y_stop2;..."
                List<string> segments = new List<string>();
                
                for (int i = 0; i < limitData.Item1.Count; i++)
                {
                    if (i < limitData.Item2.Count && i < limitData.Item3.Count && i < limitData.Item4.Count)
                    {
                        string segment = $"{limitData.Item1[i]},{limitData.Item2[i]},{limitData.Item3[i]},{limitData.Item4[i]}";
                        segments.Add(segment);
                    }
                }
                
                return string.Join(";", segments);
            }
            catch
            {
                return "Error formatting limit data";
            }
        }

        private void WriteTemplateToCsv(Dictionary<string, object> templateData, string filePath)
        {
            try
            {
                StringBuilder csvContent = new StringBuilder();
                csvContent.AppendLine("Parameter,Value");
                
                foreach (var kvp in templateData)
                {
                    string value = kvp.Value?.ToString() ?? "";
                    // Escape quotes and commas in CSV format
                    if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                    {
                        value = "\"" + value.Replace("\"", "\"\"") + "\"";
                    }
                    csvContent.AppendLine($"{kvp.Key},{value}");
                }
                
                File.WriteAllText(filePath, csvContent.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("WriteTemplateToCsv error: " + ex.Message);
                throw;
            }
        }
        
        //Disconnects from PNA and saves data settings before exiting
        private void MainWindow_Closing(object sender, EventArgs e)
        {
            PNA.Disconnect();

            Properties.Settings.Default.Protocol = (int)protocol;
            Properties.Settings.Default.Key = (int)triggerKey;
            Properties.Settings.Default.NumberLength = minNumberLength;
            Properties.Settings.Default.TimeoutEnable = timeoutEnable;
            Properties.Settings.Default.Timeout = timeout;

            Properties.Settings.Default.Save();

        }

        //Keeps certain dropdown menus from closing when clicked
        private void DropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                e.Cancel = true;
            }
        }

        //Opens help file
        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"S:\Employee Resources\Training\Engineering\One-Click Save Users Guide.pdf\");
            //Process proc = new Process();
            //proc.StartInfo = new ProcessStartInfo(@"cmd.exe");
           // pro
        }

        //Opens about dialog box
        private void aboutOneClickSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            //FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            //string version = fileVersionInfo.ProductVersion;
            string version = "1.2.1";
            string location = assembly.Location;
            string caption = "About One-Click Save";
            string text = "Version number: " + version + "\n\n" +
                          "Executable directory: " + location + "\n\n" +
                          "Please notify Doug G. about bugs or desired features.";
            MessageBox.Show(text, caption);
        }

        //Toggles enable/disable timeout
        private void enableTimeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeoutEnable = !timeoutEnable;
            enableTimeoutToolStripMenuItem.Checked = timeoutEnable;
            setTimeoutToolStripMenuItem.Enabled = timeoutEnable;
        }

    }

    public class PNA
    {
        //Socket for TCP socket connection
        private static Socket s { get; set; }

        //VISA client info for HiSLIP connection
        private static ResourceManager rMgr = new ResourceManagerClass();
        private static FormattedIO488 h = new FormattedIO488Class();

        //Local copy of protocal variable
        private static ptc protocol { get; set; }

        public static int StartClient(string IPAddr, ptc protocol)
        {
            string IPAdd = GetLocalIPAddress();
            //string IPAdd = "10.10.24.24";
            PNA.protocol = protocol;
            if (protocol == ptc.SOCKET)
            {
                // Connect to a remote device. 
                int port = 5025;
                //string IPAdd = "10.0.0.76";
                // Establish the remote endpoint for the socket.
                IPAddress ipAddress = IPAddress.Parse(IPAdd);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP  socket.  
                PNA.s = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    s.Connect(remoteEP);
                    if (!s.Connected)
                        return 1;
                    s.ReceiveTimeout = 2000;
                }
                catch
                {
                    return 1;
                }
            }
            else if (protocol == ptc.HiSLIP)
            {
                //Opens VISA client to connect to PNA via HiSLIP, port 4880, 2000 ms timeout
                string srcAddress = "tcpip0::" + IPAdd + "::hislip0,4880::instr";
                try
                {
                    h.IO = (IMessage)rMgr.Open(srcAddress, AccessMode.NO_LOCK, 2000, null);
                }
                catch
                {
                    return 1;
                }
                //src.IO.Timeout = 2000;
            }
            
            // Clear the instrument's Status Byte
            PNA.TryParse("*CLS");

            // Enable for the OPC bit (bit 0, which has weight 1) in the instrument's
            // Event Status Register, so that when that bit's value transitions from 0 to 1
            // then the Event Status Register bit in the Status Byte (bit 5 of that byte)
            // will become set.

            PNA.TryParse("*ESE 1");

            // Enable for bit 5 (which has weight 32) in the Status Byte to generate an
            // SRQ when that bit's value transitions from 0 to 1.
            PNA.TryParse("*SRE 32");

            return 0;
        }
        public static string Parse(String mssg)
        {
            //Sends message to PNA
            if (protocol == ptc.SOCKET)
            {
                // Has to be followed by a linefeed character as terminator
                mssg += "\n";

                // Encode the data string into a byte array.
                byte[] msg = Encoding.ASCII.GetBytes(mssg);

                // Send the data through the socket.  
                int bytesSent = s.Send(msg);

                // If the message was a query (involved a question mark), receive the instrument response.
                if (mssg.IndexOf("?") >= 0)
                {
                    // Buffer to store the response bytes.
                    Byte[] data = new Byte[1024];
                    // Read the batch of response bytes.
                    Int32 byteCount = s.Receive(data);
                    // String to store the response ASCII representation.
                    string responseData = System.Text.Encoding.ASCII.GetString(data, 0, byteCount);
                    return responseData;
                }
            }

            else if (protocol == ptc.HiSLIP)
            {
                h.WriteString(mssg);

                // If the message was a query (involved a question mark), receive the instrument response.
                if (mssg.IndexOf("?") >= 0)
                {
                    string responseData = h.ReadString();
                    return responseData;
                }
            }

            //If no query, return null string
            return "";
        }
        public static int TryParse(String mssg, ref string resp)
        {
            //Sends message to PNA by PNA.Parse. Returns 0 if no error and 1 if error.
            try
            {
                string response = Parse(mssg);
                resp = response;
                return 0;
            }
            catch
            {
                return 1;
            }
        }
        public static int TryParse(String mssg)
        {
            //Overload for TryParse for Write-only operations (ie. not queries).
            //Sends message to PNA by PNA.Parse. Returns 0 if no error and 1 if error.
            try
            {
                string response = Parse(mssg);
                return 0;
            }
            catch
            {
                return 1;
            }
        }
        public static int Disconnect()
        {
            //Returns 0 is successfully disconnected form socket. Returns 1 if not (ie. socket has already been disconnected)
            try
            {
                if (protocol == ptc.SOCKET)
                {
                    s.Shutdown(SocketShutdown.Both);
                    s.Close();
                }
                else if (protocol == ptc.HiSLIP)
                {
                    h.IO.Close();
                }
                return 0;
            }
            catch { return 1; }
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public static string[] parseStringArray(string unsplitString)
        {
            string[] splitString = unsplitString.Split(',');
            for (int idx = 0; idx < splitString.Length; idx++)
            {
                splitString[idx] = splitString[idx].Replace("\"", "");
                splitString[idx] = splitString[idx].Replace("\n", "");
            }
            return splitString;
        }
        public static int[] CSStoIntArray(string css)
        {
            string[] splitCSS = css.Split(',');
            return CSSAtoIntArray(splitCSS);
        }
        public static int[] CSSAtoIntArray(string[] splitCSS)
        {
            int[] stringArry = new int[splitCSS.Length];
            for (int idx = 0; idx < splitCSS.Length; idx++)
            {
                bool success = Int32.TryParse(splitCSS[idx], out stringArry[idx]);
            }
            return stringArry;
        }
        public static int SaveSNP(string saveName, int chn, string dir, bool snpOverride, bool[] snpOverrideList)
        {
            //Selects first measurement on the desired channel. It doesn't matter which
            //measurement is selected, only that the active measurement is of the desired channel.
            string measNamesStr = "";
            if (PNA.TryParse("CALC" + chn + ":PAR:CAT:EXT?", ref measNamesStr) == 1)
                return 1;

            string[] measNames = parseStringArray(measNamesStr);
            PNA.TryParse("CALC" + chn + ":PAR:SEL '" + measNames[0] + "'");

            //Fetches list of used ports based on which ports the cal sequence would cal.
            //Returns a string of syntax  +1,+2,+3\n    * \n is newline character
            string portsStr = "";
            if (PNA.TryParse("SYST:CAL:ALL:CHAN" + chn + ":PORTs?", ref portsStr) == 1)
                return 1;

            //Counts number of ports used based on length of portsStr. Used for file extention.
            int portCnt = portsStr.Length / 3;

            //Checks measurement class
            string channelClass = "";
            if (PNA.TryParse("SENS" + chn + ":CLAS:NAME?", ref channelClass) == 1)
                return 1;

            if (channelClass == "\"Standard\"\n")
            {
                var charsToRemove = new string[] { "+", "\n" };
                foreach (var c in charsToRemove)
                    portsStr = portsStr.Replace(c, string.Empty);
                PNA.TryParse("CALC" + chn + ":DATA:SNP:PORTs:SAVE \"" + portsStr + "\",\"" + dir + "\\" + saveName + ".s" + portCnt + "p\";*OPC?");
            }
            else
                PNA.TryParse("MMEMory:STORe \"" + dir + "\\" + saveName + ".s" + portCnt + "p\";*OPC?");

            return 0;
        }
    }

    public class AutoClosingMessageBox
    {
        System.Threading.Timer _timeoutTimer;
        string _caption;
        AutoClosingMessageBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            using (_timeoutTimer)
                MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }
        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }
        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }
        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }
}
