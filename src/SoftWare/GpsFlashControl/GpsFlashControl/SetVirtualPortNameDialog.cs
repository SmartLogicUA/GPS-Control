using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GpsFlashControl
{
    public partial class SetVirtualPortNameDialog : Form
    {
        public SetVirtualPortNameDialog()
        {
            InitializeComponent();
        }

        private void SetVirtualPortNameDialog_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 20; i++)
                virtualPortNameBox.Items.Add(String.Format("COM{0}", i));
            foreach (object name in System.IO.Ports.SerialPort.GetPortNames())
                virtualPortNameBox.Items.Remove(name);
        }

        public string SelectedPortName
        {
            get
            {
                
                    return (virtualPortNameBox.SelectedItem as string);
            }
        }
    }
}
