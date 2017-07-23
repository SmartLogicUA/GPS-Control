using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GpsFlashControl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainUnit());
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, except.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
