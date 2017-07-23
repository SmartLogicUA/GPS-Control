using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GpsFlashControl
{
    public partial class DeviceConfigForm : Form
    {
        System.Collections.ObjectModel.ReadOnlyCollection<char> digits = new System.Collections.ObjectModel.ReadOnlyCollection<char>(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
        SerialPortListener listener;
        DeviceDataReceivedEventHandler getTimeHandler;
        DeviceDataReceivedEventHandler getDistanceHandler;
        SetTextbox setText;
        
        public DeviceConfigForm()
        {
            InitializeComponent();
        }

        private void timeBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.TextLength > 0)
            {
                if (!digits.Contains(tb.Text[tb.TextLength - 1]))
                {
                    string tmp = tb.Text.Substring(0, tb.Text.Length - 1);
                    tb.Text = "";
                    tb.AppendText(tmp);
                }
            }

            if (tb.TextLength > 5)
            {
                string tmp = tb.Text.Substring(0, 5);
                tb.Text = "";
                tb.AppendText(tmp);
            }
        }

        private void DeviceConfigForm_Load(object sender, EventArgs e)
        {
            setText = new SetTextbox(SetText);
            getTimeHandler = new DeviceDataReceivedEventHandler(listener_GetTimeRecIntervalReceived);
            getDistanceHandler = new DeviceDataReceivedEventHandler(listener_GetDistanceRecIntervalReceived);
            
            listener = (this.Owner as MainUnit).Listener;
            listener.GetTimeRecIntervalReceived += getTimeHandler;
            listener.GetDistanceRecIntervalReceived += getDistanceHandler;
            GetTimeRecInterval(this.Owner as MainUnit);
            GetDistanceRecInterval(this.Owner as MainUnit);
        }

        void listener_GetDistanceRecIntervalReceived(object sender, StringDataReceivedEventArgs e)
        {
            this.Invoke(setText, distanceBox, e.Message);
            listener.EndListening();
        }

        void listener_GetTimeRecIntervalReceived(object sender, StringDataReceivedEventArgs e)
        {
            this.Invoke(setText, timeBox, e.Message);
            listener.EndListening();
        }

        void SetText(TextBox box, string val)
        {
            box.Text = val;
        }

        void GetTimeRecInterval(MainUnit owner)
        {
            owner.SendCommand("PCGDT");
        }

        void GetDistanceRecInterval(MainUnit owner)
        {
            owner.SendCommand("PCGDM");
        }

        private void DeviceConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            listener.GetTimeRecIntervalReceived -= getTimeHandler;
            listener.GetDistanceRecIntervalReceived -= getDistanceHandler;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            int timeInterval;
            int distanceInterval;
            if (int.TryParse(timeBox.Text, out timeInterval))
            {
                if (int.TryParse(distanceBox.Text, out distanceInterval))
                {
                    if ((timeInterval > 1) && (timeInterval <= 65000))
                    {
                        if ((distanceInterval >= 0) && (distanceInterval <= 65000))
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                            MessageBox.Show("Диапазон допустимых значений интервала записи по расстоянию от 0 до 65000 секунд", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Диапазон допустимых значений интервала записи по времени от 2 до 65000 секунд", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Диапазон допустимых значений интервала записи по расстоянию от 0 до 65000 секунд", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Диапазон допустимых значений интервала записи по времени от 2 до 65000 секунд", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    delegate void SetTextbox(TextBox box, string val);
}
