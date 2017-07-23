using System;
using System.Collections.Generic;
using System.Text;

namespace GpsFlashControl
{
    class SerialPortListener
    {
        System.IO.Ports.SerialPort port;
        bool WasOpen = true;

        public event DeviceDataReceivedEventHandler GPRMC_Received;
        public event DeviceDataReceivedEventHandler DeviceIdReceived;
        public event DeviceDataReceivedEventHandler DeviceVersionReceived;
        public event DeviceDataReceivedEventHandler ErasePageReceived;
        public event DeviceDataReceivedEventHandler EraseOkReceived;
        public event DeviceDataReceivedEventHandler PageReceived;
        public event DeviceDataReceivedEventHandler SetDefaultsOkReceived;
        public event DeviceDataReceivedEventHandler SetTimeRecIntervalOkReceived;
        public event DeviceDataReceivedEventHandler GetTimeRecIntervalReceived;
        public event DeviceDataReceivedEventHandler SetDistanceRecIntervalOkReceived;
        public event DeviceDataReceivedEventHandler GetDistanceRecIntervalReceived;
        public event DeviceDataReceivedEventHandler SetDeviceSerialSpeedOkReceived;
        public event DeviceDataReceivedEventHandler DeviceErrorReceived;

        protected virtual void OnDeviceIdReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (DeviceIdReceived != null)
                DeviceIdReceived(sender, e);
        }

        protected virtual void OnDeviceVersionReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (DeviceVersionReceived != null)
                DeviceVersionReceived(sender, e);
        }

        protected virtual void OnErasePageReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (ErasePageReceived != null)
                ErasePageReceived(sender, e);
        }

        protected virtual void OnEraseOkReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (EraseOkReceived != null)
                EraseOkReceived(sender, e);
        }

        protected virtual void OnPageReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (PageReceived != null)
                PageReceived(sender, e);
        }

        protected virtual void OnSetDefaultsOkReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (SetDefaultsOkReceived != null)
                SetDefaultsOkReceived(sender, e);
        }

        protected virtual void OnSetTimeRecIntervalOkReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (SetTimeRecIntervalOkReceived != null)
                SetTimeRecIntervalOkReceived(sender, e);
        }

        protected virtual void OnGetTimeRecIntervalReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (GetTimeRecIntervalReceived != null)
                GetTimeRecIntervalReceived(sender, e);
        }

        protected virtual void OnSetDistanceRecIntervalOkReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (SetDistanceRecIntervalOkReceived != null)
                SetDistanceRecIntervalOkReceived(sender, e);
        }

        protected virtual void OnGetDistanceRecIntervalReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (GetDistanceRecIntervalReceived != null)
                GetDistanceRecIntervalReceived(sender, e);
        }

        protected virtual void OnSetDeviceSerialSpeedOkReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (SetDeviceSerialSpeedOkReceived != null)
                SetDeviceSerialSpeedOkReceived(sender, e);
        }

        protected virtual void OnDeviceErrorReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (DeviceErrorReceived != null)
                DeviceErrorReceived(sender, e);
        }

        public SerialPortListener(System.IO.Ports.SerialPort port)
        {
            this.port = port;
        }

        public void BeginListening()
        {
                port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
                if (!port.IsOpen)
                {
                    try
                    {
                        port.Open();
                        WasOpen = false;
                    }
#pragma warning disable 168
                    catch (System.IO.IOException except)
                    {
                        System.Windows.Forms.MessageBox.Show("Пожалуйста выберите порт", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        System.Windows.Forms.Application.Restart();
                    }
#pragma warning restore 168
                }
                else
                   WasOpen = true;
        }

        public void EndListening()
        {
            port.DataReceived -= this.port_DataReceived;
            if (!WasOpen)
            {
                port.Close();
                WasOpen = true;
            }
        }


        void port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                port.ReadTo("$");
                string message = port.ReadLine();
                if (IsCorrectCRC(message))
                {
                    message = message.Substring(0, message.Length - 3);
                    //if (message.Substring(0, 5) == "GPRMC")
                    //    OnGPRMC_Received(this, new StringDataReceivedEventArgs(message));
                    switch (message.Substring(0, 5))
                    {
                        case "SYHND":
                            OnDeviceIdReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                            break;
                        case "SYVER":
                            OnDeviceVersionReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                            break;
                        case "SYCEF":
                            if (message.Length >= 8)
                            {
                                if (message.Substring(6, 2) == "OK")
                                    OnEraseOkReceived(this, null);
                                else
                                    OnErasePageReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                            }
                            else
                                OnErasePageReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                            break;
                        case "SYREP":
                            if (message.Substring(6) == "ERROR")
                                OnDeviceErrorReceived(this, new StringDataReceivedEventArgs("Неверный номер страницы памяти"));
                            else
                                OnPageReceived(this, new StringDataReceivedEventArgs(message.Substring(5)));
                            break;
                        case "SYSFS":
                            if (message.Substring(6, 2) == "OK")
                                OnSetDefaultsOkReceived(this, null);
                            else
                                OnDeviceErrorReceived(this, new StringDataReceivedEventArgs("Во время установки параметров по умолчанию произошла ошибка"));
                            break;
                        case "SYSDT":
                            if (message.Substring(6, 2) == "OK")
                                OnSetTimeRecIntervalOkReceived(this, null);
                            else
                                OnDeviceErrorReceived(this, new StringDataReceivedEventArgs("При установке интервала записи по времени произошла ошибка"));
                            break;
                        case "SYGDT":
                            OnGetTimeRecIntervalReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                            break;
                        case "SYSDM":
                            if (message.Substring(6, 2) == "OK")
                                OnSetDistanceRecIntervalOkReceived(this, null);
                            else
                                OnDeviceErrorReceived(this, new StringDataReceivedEventArgs("При установке интервала записи по расстоянию произошла ошибка"));
                            break;
                        case "SYGDM":
                            OnGetDistanceRecIntervalReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                            break;
                        case "SYSCS":
                            if (message.Substring(6, 2) == "OK")
                                OnSetDeviceSerialSpeedOkReceived(this, null);
                            else
                                OnDeviceErrorReceived(this, new StringDataReceivedEventArgs("При установке скорости работы устройства произошла ошибка"));
                            break;
                        case "SYCOM":
                            if (message == "SYCOMMAND NOT SUPPORT")
                            {
                                OnDeviceErrorReceived(this, new StringDataReceivedEventArgs("Команда не поддерживается"));
                            }
                            break;
                    }
                }
            }
#pragma warning disable 168
            catch (Exception except)
            {
            }
#pragma warning restore 168
        }

        protected void OnGPRMC_Received(object sender, StringDataReceivedEventArgs e)
        {
            if (GPRMC_Received != null)
                GPRMC_Received(sender, e);
        }

        public static string CalculateCRC(string data)
        {
            byte crc = 0;
            foreach (byte b in Encoding.ASCII.GetBytes(data))
            {
                if (b == 0x3f)
                    crc ^= 0xff;
                else
                    crc ^= b;
            }
            return crc.ToString("X2");
        }

        static bool IsCorrectCRC(string message)
        {
            if (CalculateCRC(message.Substring(0, message.Length - 3)) == message.Substring(message.Length - 2, 2))
                return true;
            else
                return false;
        }
    }

    class StringDataReceivedEventArgs : EventArgs
    {
        string message = "";

        public StringDataReceivedEventArgs(string message)
        {
            this.message = message;
        }

        public string Message
        {
            get
            {
                return message;
            }
        }
    }

    delegate void DeviceDataReceivedEventHandler(object sender, StringDataReceivedEventArgs e);
}
