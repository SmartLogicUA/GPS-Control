using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GpsFlashControl
{
    public partial class SetPortDialog : Form
    {
        public SetPortDialog()
        {
            InitializeComponent();
        }

        private void SetPortDialog_Load(object sender, EventArgs e)
        {
            portNameBox.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
        }

        public string SelectedPort
        {
            get
            {
                  return portNameBox.SelectedItem as string;
            }
        }
    }
}
