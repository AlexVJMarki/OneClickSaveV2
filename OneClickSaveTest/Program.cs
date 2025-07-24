using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace OneClickSaveTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /// check if given exe alread running or not
            /// 
            /// returns true if already running
            bool IsAlreadyRunning()
            {
                Process thisProc = Process.GetCurrentProcess();
                
                // Check how many total processes have the same name as the current one
                // Checks for 2 or greater because it always counts itself
                if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
                {
                    return true;
                }
                
                return false;
            }

            if (IsAlreadyRunning())
            {
                string message = "One-Click Save";
                string caption = "Application already running.\n\nWould you like to open a new window?";
                DialogResult result;
                result = MessageBox.Show(caption, message, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new OneClickSaveForm());
        }
    }
}
