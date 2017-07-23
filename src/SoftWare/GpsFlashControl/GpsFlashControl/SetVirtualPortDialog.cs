using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GpsFlashControl
{
    public partial class SetVirtualPortDialog : Form
    {
        SetVirtualPortNameDialog portDialog;
        VoidString setOffBtnStatus;
        SerialNET.OnOpened onOpenedHandler;
        SerialNET.OnClosed onClosedHandler;
        
        public SetVirtualPortDialog()
        {
            InitializeComponent();
        }

        private void SetVirtualPortDialog_Load(object sender, EventArgs e)
        {
            portDialog = new SetVirtualPortNameDialog();
            setOffBtnStatus = new VoidString(SetOffBtnStatus);
            MainUnit mainform = this.Owner as MainUnit;
            if (mainform.VirtualPort.ComPort!=0)
                portNameLbl.Text = String.Format("COM{0}", mainform.VirtualPort.ComPort);
            onBtn.Enabled = !mainform.VirtualPort.Created;
            if (mainform.DummyTimerEnabled || mainform.WorkingTimerEnabled)
            {
            }
            else
                offBtn.Enabled = !onBtn.Enabled;
            selectNewPortBtn.Enabled = onBtn.Enabled;
            onOpenedHandler = new SerialNET.OnOpened(VirtualPort_OnOpened);
            onClosedHandler = new SerialNET.OnClosed(VirtualPort_OnClosed);
            mainform.VirtualPort.OnOpened += onOpenedHandler;
            mainform.VirtualPort.OnClosed += onClosedHandler;
            //if (mainform.DummyTimer.Enabled)
            //    offBtn.Enabled = false;
        }

        void VirtualPort_OnClosed()
        {
            this.Invoke(setOffBtnStatus, "true");
        }

        void VirtualPort_OnOpened()
        {
            this.Invoke(setOffBtnStatus, "false");
        }

        private void selectNewPortBtn_Click(object sender, EventArgs e)
        {
            if (portDialog.ShowDialog() == DialogResult.OK)
            {
                portNameLbl.Text = portDialog.SelectedPortName;
                if (offBtn.Enabled)
                    offBtn_Click(null, null);
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void onBtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(portNameLbl.Text))
                MessageBox.Show("Сначала выберите новый порт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                onBtn.Enabled = false;
                selectNewPortBtn.Enabled = false;
                MainUnit mainform = this.Owner as MainUnit;
                mainform.VirtualPort.ComPort = int.Parse(portNameLbl.Text.Substring(3));
                mainform.VirtualPort.Created = true;
                offBtn.Enabled = true;
            }
        }

        private void offBtn_Click(object sender, EventArgs e)
        {
            MainUnit mainform = this.Owner as MainUnit;
            mainform.VirtualPort.Created = false;
            onBtn.Enabled = true;
            offBtn.Enabled = false;
            selectNewPortBtn.Enabled = true;
        }

        private void SetOffBtnStatus(string input)
        {
            offBtn.Enabled = bool.Parse(input);
        }

        private void SetVirtualPortDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainUnit mainform = this.Owner as MainUnit;
            mainform.VirtualPort.OnOpened -= onOpenedHandler;
            mainform.VirtualPort.OnClosed -= onClosedHandler;
        }

    }
}
